using Expenzio.Application.Common.Interfaces;
using Expenzio.Domain.Entities;
using Expenzio.Domain.Events;

namespace Expenzio.Application.Expenses.Commands.CreateExpense;

public record CreateExpenseCommand : IRequest<int>
{
    public int ListId { get; init; }

    public string? Title { get; init; }
}

public class CreateTodoItemCommandHandler : IRequestHandler<CreateExpenseCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        var entity = new Expense
        {
            Id = 0
        };

        entity.AddDomainEvent(new ExpenseCreatedEvent(entity));

        _context.CreateSet<Expense>().Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
