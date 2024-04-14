using Expenzio.Domain.Entities;
using Expenzio.Service.Interfaces;

namespace Expenzio.Api.Controllers.GraphQLApi;

[ExtendObjectType(typeof(BaseQuery))]
public class ExpenseCategoryQuery
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
