using Expenzio.Application.Common.Interfaces;
using Expenzio.Domain.Entities;

namespace Expenzio.Application.ExpenseCategories.Commands.CreateExpenseCategory;

public class CreateExpenseCategoryCommand : IRequest<int>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}

public class CreateExpenseCategoryCommandHandler : IRequestHandler<CreateExpenseCategoryCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly DbSet<ExpenseCategory> _expenseCategories;
    private readonly IUser _currentUserService;

    public CreateExpenseCategoryCommandHandler(IApplicationDbContext context, IUser currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
        _expenseCategories = _context.CreateSet<ExpenseCategory>();
    }

    public async Task<int> Handle(CreateExpenseCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = new ExpenseCategory
        {
            Name = request.Name,
            Description = request.Description,
            UserId = Guid.Parse(_currentUserService.Id!)
        };

        _expenseCategories.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
