using Expenzio.Domain.Entities;
using Expenzio.Service.Interfaces;

namespace Expenzio.Api.Controllers.GraphQLApi;

[ExtendObjectType(typeof(BaseQuery))]
public class ExpenseQuery
{
    public Expense GetExpense(Guid id) {
        return new() {
            Id = Guid.NewGuid(),
            Description = "Expense description",
            Amount = 100,
            MonetaryUnit = "USD",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            IsDeleted = false,
            CategoryId = Guid.NewGuid()
        };
    }

    public async Task<IEnumerable<Expense>> GetExpenses([Service] IExpenseService _expenseService) {
        return await _expenseService.GetExpensesAsync(); 
    }
}
