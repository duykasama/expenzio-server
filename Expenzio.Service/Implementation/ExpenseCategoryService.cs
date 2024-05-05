using expenzio.DAL.Interfaces;
using Expenzio.Domain.Entities;
using Expenzio.Domain.Models.Requests;
using Expenzio.Domain.Models.Responses;
using Expenzio.Service.Interfaces;

namespace Expenzio.Service.Implementation;

/// <inheritdoc />
public class ExpenseCategoryService : IExpenseCategoryService
{
    private readonly IExpenseCategoryRepository _categoryRepository;

    public ExpenseCategoryService(IExpenseCategoryRepository categoryRespository)
    {
        _categoryRepository = categoryRespository;
    }

    /// <inheritdoc />
    public Task<ApiResponse> CreateExpenseCategoryAsync(CreateExpenseCategoryRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ExpenseCategory>> GetCategoriesAsync()
    {
        return await _categoryRepository.GetAllAsync();
    }
}
