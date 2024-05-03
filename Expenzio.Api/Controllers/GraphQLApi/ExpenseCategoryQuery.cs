using Expenzio.Domain.Entities;
using Expenzio.Service.Interfaces;

namespace Expenzio.Api.Controllers.GraphQLApi;

/// <summary>
/// Represents the expense category query.
/// </summary>
/// <remarks>
/// This class contains queries for expense categories.
/// </remarks>
[ExtendObjectType(typeof(BaseQuery))]
public sealed class ExpenseCategoryQuery
{
    public ExpenseCategory GetExpenseCategory()
    {
        return new()
        {
            Id = Guid.NewGuid(),
            Name = "Expense category name",
            Description = "Expense category description",
        };
    }

    public async Task<IEnumerable<ExpenseCategory>> GetExpenseCategories([Service] IExpenseCategoryService _expenseCategoryService)
    {
        return await _expenseCategoryService.GetCategoriesAsync();
    }
}
