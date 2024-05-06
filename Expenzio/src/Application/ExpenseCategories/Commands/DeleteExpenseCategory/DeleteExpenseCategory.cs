using Expenzio.Application.Common.Interfaces;
using Expenzio.Domain.Entities;
using Expenzio.Domain.Events;

namespace Expenzio.Application.ExpenseCategories.Commands.DeleteExpenseCategory;

public record DeleteExpenseCategoryCommand(int Id) : IRequest;

public class DeleteExpenseCategoryCommandHandler : IRequestHandler<DeleteExpenseCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteExpenseCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteExpenseCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.CreateSet<Expense>()
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.CreateSet<Expense>().Remove(entity);

        entity.AddDomainEvent(new ExpenseDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
