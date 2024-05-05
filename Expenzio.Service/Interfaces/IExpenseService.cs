using Expenzio.Common.Attributes;
using Expenzio.Domain.Entities;
using Expenzio.Domain.Models.Requests.Expense;
using Microsoft.Extensions.DependencyInjection;

namespace Expenzio.Service.Interfaces;

/// <summary>
/// Represents the expense service.
/// </summary>
/// <remarks>
/// This interface contains methods for expenses.
/// </remarks>
[AutoRegister(ServiceLifetime.Scoped)]
public interface IExpenseService
{
    /// <summary>
    /// Get all expenses
    /// </summary>
    /// <returns>Task of IEnumerable of Expense</returns>
    Task<IEnumerable<Expense>> GetExpensesAsync();

    /// <summary>
    /// Get paginated expenses
    /// </summary>
    /// <returns>Task of IQueryable of Expense</returns>
    Task<IQueryable<Expense>> GetPaginatedExpensesAsync();
    Task<Expense> GetExpenseAsync(Guid id);
    Task<Expense> AddExpenseAsync(CreateExpenseRequest request, CancellationToken cancellationToken = default);
    Task<Expense> UpdateExpenseAsync(Expense expense);
    Task DeleteExpenseAsync(Guid id);

    /// <summary>
    /// Get daily expenses by user id
    /// </summary>
    /// <returns>Task of IQueryable of Expense</returns>
    Task<IQueryable<Expense>> GetDailyExpensesByUserIdAsync();

    /// <summary>
    /// Get weekly expenses by user id
    /// </summary>
    /// <returns>Task of IQueryable of Expense</returns>
    Task<IQueryable<Expense>> GetWeeklyExpensesAsync();

    /// <summary>
    /// Get monthly expenses by user id
    /// </summary>
    /// <returns>Task of IQueryable of Expense</returns>
    Task<IQueryable<Expense>> GetMonthlyExpensesAsync();
}
