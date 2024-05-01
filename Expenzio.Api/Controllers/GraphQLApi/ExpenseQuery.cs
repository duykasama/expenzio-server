using Expenzio.Domain.Entities;
using Expenzio.Service.Interfaces;
using HotChocolate.Authorization;

namespace Expenzio.Api.Controllers.GraphQLApi;

[ExtendObjectType(typeof(BaseQuery))]
[Authorize]
public class ExpenseQuery
{
    public async Task<Expense> GetExpense(Guid id, [Service] IExpenseService _expenseService)
    {
        return await _expenseService.GetExpenseAsync(id);
    }

    [UseOffsetPaging]
    public async Task<IQueryable<Expense>> GetExpenses([Service] IExpenseService _expenseService)
    {
        return await _expenseService.GetPaginatedExpensesAsync(); 
    }

    [UseOffsetPaging]
    public async Task<IQueryable<Expense>> GetDailyExpenses([Service] IExpenseService _expenseService)
    {
        return await _expenseService.GetDailyExpensesByUserIdAsync();
    }
}
