using System.Security.Claims;
using AutoMapper;
using Expenzio.Common.Exceptions;
using Expenzio.DAL.Interfaces;
using Expenzio.Domain.Entities;
using Expenzio.Domain.Models.Requests.Expense;
using Expenzio.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Expenzio.Service;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IMapper _mapper;
    private readonly HttpContext _httpContext;
    private readonly IUserRepository _userRepository;

    public ExpenseService(
        IExpenseRepository expenseRepository,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        IUserRepository userRepository
    )
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
        _httpContext = httpContextAccessor.HttpContext;
        _userRepository = userRepository;
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

    // TODO: Unit test
    public async Task<IQueryable<Expense>> GetDailyExpensesByUserIdAsync()
    {
        var nameIdentifierClaim = _httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(nameIdentifierClaim?.Value)) throw new UnauthorizedException("User is not authenticated");
        var userId = Guid.Parse(nameIdentifierClaim.Value);
        var userExists = await _userRepository.ExistsAsync(u => !u.IsDeleted && u.Id == userId);
        if (!userExists) throw new NotFoundException("User not found");
        var expenses = (await _expenseRepository.GetAllAsync())
            .Where(e => e.UserId == userId && e.CreatedAt.Date == DateTime.UtcNow.Date);
        
        return expenses;
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

    public async Task<IQueryable<Expense>> GetPaginatedExpensesAsync()
    {
        return (await _expenseRepository.GetAllAsync())
            .Include(e => e.Category);
    }

    public Task<Expense> UpdateExpenseAsync(Expense expense)
    {
        throw new NotImplementedException();
    }
}
