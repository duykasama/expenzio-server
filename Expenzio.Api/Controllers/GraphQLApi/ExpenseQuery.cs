using Expenzio.Domain.Entities;
using Expenzio.Service.Interfaces;

namespace Expenzio.Api.Controllers.GraphQLApi;

[ExtendObjectType(typeof(BaseQuery))]
public class ExpenseQuery
{
    public async Task<Expense> GetExpense(Guid id, [Service] IExpenseService _expenseService) {
        return await _expenseService.GetExpenseAsync(id);
    }

    [UseOffsetPaging]
    public async Task<IQueryable<Expense>> GetExpenses([Service] IExpenseService _expenseService) {
        return await _expenseService.GetPaginatedExpensesAsync(); 
    }
}
