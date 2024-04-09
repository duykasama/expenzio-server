using expenzio.DAL.Interfaces;
using Expenzio.DAL.Data;
using Expenzio.Domain.Entities;

namespace Expenzio.DAL.Implementations;

public class ExpenseCategoryRepository : GenericRepository<ExpenseCategory>, IExpenseCategoryRepository
{
    public ExpenseCategoryRepository(ExpenzioDbContext context) : base(context)
    {
    }
}
