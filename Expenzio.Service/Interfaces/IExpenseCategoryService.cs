using Expenzio.Common.Interfaces;
using Expenzio.Domain.Entities;

namespace Expenzio.Service.Interfaces;

public interface IExpenseCategoryService : IAutoRegisterable
{
    /// <summary>
    /// Get all expense categories
    /// </summary>
    /// <returns>Task of IEnumerable of ExpenseCategory</returns>
    Task<IEnumerable<ExpenseCategory>> GetCategoriesAsync();
}
