using expenzio.DAL.Interfaces;
using Expenzio.Domain.Entities;
using Expenzio.Service.Interfaces;

namespace Expenzio.Service.Implementation;

public class ExpenseCategoryService : IExpenseCategoryService
{
    private readonly IExpenseCategoryRepository _categoryRepository;

    public ExpenseCategoryService(IExpenseCategoryRepository categoryRespository)
    {
        _categoryRepository = categoryRespository;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ExpenseCategory>> GetCategoriesAsync()
    {
        return await _categoryRepository.GetAllAsync();
    }
}
