using Expenzio.DAL.Interfaces;
using Expenzio.Domain.Entities;
using Expenzio.Service.Interfaces;

namespace Expenzio.Service;

public class ExpenseService : IExpenseService
{
		private readonly IExpenseRepository _expenseRepository;

		public ExpenseService(IExpenseRepository expenseRepository)
		{
				_expenseRepository = expenseRepository;
		}

		public Task<Expense> AddExpenseAsync(Expense expense)
		{
						throw new NotImplementedException();
		}

		public Task DeleteExpenseAsync(int id)
		{
						throw new NotImplementedException();
		}

		public Task<Expense> GetExpenseAsync(int id)
		{
						throw new NotImplementedException();
		}

		public async Task<IEnumerable<Expense>> GetExpensesAsync()
		{
				return await _expenseRepository.GetAllAsync();
		}

		public Task<Expense> UpdateExpenseAsync(Expense expense)
		{
						throw new NotImplementedException();
		}
}
