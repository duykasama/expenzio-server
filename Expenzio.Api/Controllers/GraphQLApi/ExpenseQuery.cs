using Expenzio.Domain.Entities;
using Expenzio.Service.Interfaces;
using HotChocolate.Authorization;

namespace Expenzio.Api.Controllers.GraphQLApi;

/// <summary>
/// Represents the expense query.
/// </summary>
/// <remarks>
/// This class contains queries for expenses.
/// </remarks>
[ExtendObjectType(typeof(BaseQuery))]
[Authorize]
public sealed class ExpenseQuery
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

    [UseOffsetPaging]
    public async Task<IQueryable<Expense>> GetWeeklyExpenses([Service] IExpenseService _expenseService)
    {
        return await _expenseService.GetWeeklyExpensesAsync();
    }
}
