using Expenzio.Common.Interfaces;
using Expenzio.Domain.Entities;

namespace Expenzio.Service.Interfaces;

/// <summary>
/// Represents the expense category service.
/// </summary>
/// <remarks>
/// This interface contains methods for expense categories.
/// </remarks>
public interface IExpenseCategoryService : IAutoRegisterable
{
    /// <summary>
    /// Get all expense categories
    /// </summary>
    /// <returns>Task of IEnumerable of ExpenseCategory</returns>
    Task<IEnumerable<ExpenseCategory>> GetCategoriesAsync();
}
