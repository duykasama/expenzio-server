using Expenzio.Application.Common.Interfaces;
using Expenzio.Domain.Entities;
using Expenzio.Domain.Events;

namespace Expenzio.Application.Expenses.Commands.CreateExpense;

public record CreateExpenseCommand : IRequest<Guid>
{
    public int ListId { get; init; }

    public string? Title { get; init; }
}

public class CreateTodoItemCommandHandler : IRequestHandler<CreateExpenseCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly DbSet<Expense> _expenses;

    public CreateTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
        _expenses = _context.CreateSet<Expense, Guid>();
    }

    public async Task<Guid> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        var entity = new Expense
        {
            Id = Guid.NewGuid(),
        };

        entity.AddDomainEvent(new ExpenseCreatedEvent(entity));

        _expenses.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
