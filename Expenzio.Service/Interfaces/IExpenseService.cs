using Expenzio.Common.Interfaces;
using Expenzio.Domain.Entities;
using Expenzio.Domain.Models.Requests.Expense;

namespace Expenzio.Service.Interfaces;

public interface IExpenseService : IAutoRegisterable
{
    Task<IEnumerable<Expense>> GetExpensesAsync();
    Task<IQueryable<Expense>> GetPaginatedExpensesAsync();
    Task<Expense> GetExpenseAsync(Guid id);
    Task<Expense> AddExpenseAsync(CreateExpenseRequest request, CancellationToken cancellationToken = default);
    Task<Expense> UpdateExpenseAsync(Expense expense);
    Task DeleteExpenseAsync(Guid id);
}
