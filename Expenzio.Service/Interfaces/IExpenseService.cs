using Expenzio.Common.Interfaces;
using Expenzio.Domain.Entities;

namespace Expenzio.Service.Interfaces;

public interface IExpenseService : IAutoRegisterable
{
		Task<IEnumerable<Expense>> GetExpensesAsync();
		Task<Expense> GetExpenseAsync(int id);
		Task<Expense> AddExpenseAsync(Expense expense);
		Task<Expense> UpdateExpenseAsync(Expense expense);
		Task DeleteExpenseAsync(int id);
}
