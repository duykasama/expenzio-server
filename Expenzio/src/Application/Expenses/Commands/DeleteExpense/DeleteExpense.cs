using Expenzio.Application.Common.Interfaces;
using Expenzio.Domain.Entities;
using Expenzio.Domain.Events;

namespace Expenzio.Application.Expenses.Commands.DeleteExpense;

public record DeleteExpenseCommand(int Id) : IRequest;

public class DeleteExpenseCommandHandler : IRequestHandler<DeleteExpenseCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteExpenseCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.CreateSet<Expense>()
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.CreateSet<Expense>().Remove(entity);

        entity.AddDomainEvent(new ExpenseDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
