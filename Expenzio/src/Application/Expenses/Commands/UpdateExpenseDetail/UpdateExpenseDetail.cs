using Expenzio.Application.Common.Interfaces;
using Expenzio.Domain.Entities;
using Expenzio.Domain.Enums;

namespace Expenzio.Application.Expenses.Commands.UpdateExpenseDetail;

public record UpdateExpenseDetailCommand : IRequest
{
    public Guid Id { get; init; }

    public Guid ListId { get; init; }

    public PriorityLevel Priority { get; init; }

    public string? Note { get; init; }
}

public class UpdateExpenseDetailCommandHandler : IRequestHandler<UpdateExpenseDetailCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly DbSet<Expense> _expenses;

    public UpdateExpenseDetailCommandHandler(IApplicationDbContext context)
    {
        _context = context;
        _expenses = _context.CreateSet<Expense, Guid>();
    }

    public async Task Handle(UpdateExpenseDetailCommand request, CancellationToken cancellationToken)
    {
        var entity = await _expenses
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
