using Expenzio.Application.Common.Interfaces;
using Expenzio.Domain.Entities;

namespace Expenzio.Application.Expenses.Commands.UpdateExpense;

public record UpdateExpenseCommand : IRequest
{
    public Guid Id { get; init; }

    public string? Title { get; init; }

    public bool Done { get; init; }
}

public class UpdateExpenseCommandHandler : IRequestHandler<UpdateExpenseCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly DbSet<Expense> _expenses;

    public UpdateExpenseCommandHandler(IApplicationDbContext context)
    {
        _context = context;
        _expenses = _context.CreateSet<Expense, Guid>();
    }

    public async Task Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
    {
        var entity = await _expenses
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
