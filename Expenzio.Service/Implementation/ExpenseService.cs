using AutoMapper;
using Expenzio.DAL.Interfaces;
using Expenzio.Domain.Entities;
using Expenzio.Domain.Models.Requests.Expense;
using Expenzio.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Expenzio.Service;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IMapper _mapper;

    public ExpenseService(IExpenseRepository expenseRepository, IMapper mapper)
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
    }

    public async Task<Expense> AddExpenseAsync(CreateExpenseRequest request, CancellationToken cancellationToken = default)
    {
        var expense = _mapper.Map<Expense>(request);
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
        return (await _expenseRepository.GetAllAsync())
            .Include(e => e.Category);
    }

    public Task<Expense> UpdateExpenseAsync(Expense expense)
    {
        throw new NotImplementedException();
    }
}
