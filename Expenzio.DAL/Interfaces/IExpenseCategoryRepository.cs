using Expenzio.Common.Interfaces;
using Expenzio.DAL.Interfaces;
using Expenzio.Domain.Entities;

namespace expenzio.DAL.Interfaces;

public interface IExpenseCategoryRepository : IGenericRepository<ExpenseCategory>, IAutoRegisterable
{
}
