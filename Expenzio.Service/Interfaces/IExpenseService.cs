using Expenzio.Common.Interfaces;
using Expenzio.Domain.Entities;

namespace Expenzio.Service.Interfaces;

public interface IExpenseService : IAutoRegisterable
{
    Task<IEnumerable<Expense>> GetExpensesAsync();
    Task<Expense> GetExpenseAsync(Guid id);
    Task<Expense> AddExpenseAsync(Expense expense, CancellationToken cancellationToken = default);
    Task<Expense> UpdateExpenseAsync(Expense expense);
    Task DeleteExpenseAsync(Guid id);
}
