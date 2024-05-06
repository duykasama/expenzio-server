using Expenzio.Application.Common.Interfaces;
using Expenzio.Domain.Entities;

namespace Expenzio.Application.ExpenseCategories.Commands.UpdateExpenseCategory;

public record UpdateExpenseCategoryCommand : IRequest
{
    public int Id { get; init; }

    public string? Title { get; init; }

    public bool Done { get; init; }
}

public class UpdateExpenseCategoryCommandHandler : IRequestHandler<UpdateExpenseCategoryCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly DbSet<ExpenseCategory> _categories;

    public UpdateExpenseCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
        _categories = _context.CreateSet<ExpenseCategory>();
    }

    public async Task Handle(UpdateExpenseCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _categories
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
