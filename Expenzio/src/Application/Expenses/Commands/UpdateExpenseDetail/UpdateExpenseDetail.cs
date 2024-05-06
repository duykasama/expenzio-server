using Expenzio.Application.Common.Interfaces;
using Expenzio.Domain.Entities;
using Expenzio.Domain.Enums;

namespace Expenzio.Application.Expenses.Commands.UpdateExpenseDetail;

public record UpdateExpenseDetailCommand : IRequest
{
    public int Id { get; init; }

    public int ListId { get; init; }

    public PriorityLevel Priority { get; init; }

    public string? Note { get; init; }
}

public class UpdateExpenseDetailCommandHandler : IRequestHandler<UpdateExpenseDetailCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateExpenseDetailCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateExpenseDetailCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.CreateSet<Expense>()
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
