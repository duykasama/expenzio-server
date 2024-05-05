using Expenzio.Common.Interfaces;
using Expenzio.Domain.Entities;
using Expenzio.Domain.Models.Requests;
using Expenzio.Domain.Models.Responses;

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
    /// Create an expense category
    /// </summary>
    /// <param name="request">The request to create an expense category</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>Task of ApiResponse</returns>
    Task<ApiResponse> CreateExpenseCategoryAsync(CreateExpenseCategoryRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Get all expense categories
    /// </summary>
    /// <returns>Task of IEnumerable of ExpenseCategory</returns>
    Task<IEnumerable<ExpenseCategory>> GetCategoriesAsync();
}
