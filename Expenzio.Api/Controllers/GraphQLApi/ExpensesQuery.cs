using Expenzio.Domain.Entities;
using Expenzio.Service.Interfaces;

namespace Expenzio.Controllers.GraphQLApi;

public class ExpensesQuery {
		public Expense GetExpense() {
				return new Expense {
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
				var result = await _expenseService.GetExpensesAsync(); 
				return result;
		}
}
