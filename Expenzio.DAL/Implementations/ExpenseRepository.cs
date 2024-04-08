using Expenzio.DAL.Interfaces;
using Expenzio.DAL.Data;
using Expenzio.Domain.Entities;

namespace Expenzio.DAL.Implementations;

public class ExpenseRepository : GenericRepository<Expense>, IExpenseRepository
{
		public ExpenseRepository(ExpenzioDbContext context) : base(context)
		{
		}
}
