using Expenzio.Application.Common.Interfaces;
using Expenzio.Domain.Entities;
using Expenzio.Domain.Events;

namespace Expenzio.Application.ExpenseCategories.Commands.DeleteExpenseCategory;

public record DeleteExpenseCategoryCommand(Guid Id) : IRequest;

public class DeleteExpenseCategoryCommandHandler : IRequestHandler<DeleteExpenseCategoryCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly DbSet<ExpenseCategory> _expenseCategories;

    public DeleteExpenseCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
        _expenseCategories = _context.CreateSet<ExpenseCategory, Guid>();
    }

    public async Task Handle(DeleteExpenseCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _expenseCategories
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _expenseCategories.Remove(entity);

        entity.AddDomainEvent(new ExpenseCategoryDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
