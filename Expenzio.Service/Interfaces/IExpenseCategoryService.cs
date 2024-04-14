using Expenzio.Common.Interfaces;
using Expenzio.Domain.Entities;

namespace Expenzio.Service.Interfaces;

public interface IExpenseCategoryService : IAutoRegisterable
{
    Task<IEnumerable<ExpenseCategory>> GetCategoriesAsync();
}
