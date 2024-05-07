using Expenzio.Application.Common.Interfaces;
using Expenzio.Domain.Entities;
using Expenzio.Domain.Events;

namespace Expenzio.Application.Expenses.Commands.DeleteExpense;

public record DeleteExpenseCommand(Guid Id) : IRequest;

public class DeleteExpenseCommandHandler : IRequestHandler<DeleteExpenseCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly DbSet<Expense> _expenses;

    public DeleteExpenseCommandHandler(IApplicationDbContext context)
    {
        _context = context;
        _expenses = _context.CreateSet<Expense, Guid>();
    }

    public async Task Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
    {
        var entity = await _expenses
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _expenses.Remove(entity);

        entity.AddDomainEvent(new ExpenseDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
