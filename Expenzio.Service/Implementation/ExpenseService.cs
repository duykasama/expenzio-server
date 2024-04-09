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

    public async Task<Expense> AddExpenseAsync(Expense expense, CancellationToken cancellationToken = default)
    {
        await _expenseRepository.AddAsync(expense, cancellationToken);
        return expense;
    }

    public Task DeleteExpenseAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Expense> GetExpenseAsync(Guid id)
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
