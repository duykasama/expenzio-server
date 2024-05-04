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

/// <inheritdoc />
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

    /// <inheritdoc />
    public async Task<Expense> AddExpenseAsync(CreateExpenseRequest request, CancellationToken cancellationToken = default)
    {
        var expense = _mapper.Map<Expense>(request);
        await _expenseRepository.AddAsync(expense, cancellationToken);
        return expense;
    }

    /// <inheritdoc />
    public Task DeleteExpenseAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    // TODO: Unit test
    /// <inheritdoc />
    public async Task<IQueryable<Expense>> GetDailyExpensesByUserIdAsync()
    {
        var userId = await GetUserIdFromRequest();
        var expenses = (await _expenseRepository.GetAllAsync())
            .Where(e => e.UserId == userId && e.CreatedAt.Date == DateTime.UtcNow.Date);
        
        return expenses;
    }

    /// <inheritdoc />
    public Task<Expense> GetExpenseAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Expense>> GetExpensesAsync()
    {
        return (await _expenseRepository.GetAllAsync())
            .Include(e => e.Category);
    }

    /// <inheritdoc />
    public async Task<IQueryable<Expense>> GetMonthlyExpensesAsync()
    {
        var userId = await GetUserIdFromRequest();
        var firstDateOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        var lastDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
        var expenses = await _expenseRepository
            .FindAsync(e => e.UserId == userId && e.CreatedAt >= firstDateOfMonth && e.CreatedAt <= lastDayOfMonth);
        return expenses;
    }

    /// <inheritdoc />
    public async Task<IQueryable<Expense>> GetPaginatedExpensesAsync()
    {
        return (await _expenseRepository.GetAllAsync())
            .Include(e => e.Category);
    }

    /// <inheritdoc />
    public async Task<IQueryable<Expense>> GetWeeklyExpensesAsync()
    {
        var userId = await GetUserIdFromRequest();
        var today = DateTime.Today;
        int diff = DayOfWeek.Monday - today.DayOfWeek;
        if (diff > 0) diff -= 7;
        var monday = today.AddDays(diff);
        var sunday = monday.AddDays(6);
        var expenses = await _expenseRepository
            .FindAsync(e => e.UserId == userId && e.CreatedAt >= monday && e.CreatedAt <= sunday);
        return expenses;
    }

    /// <inheritdoc />
    public Task<Expense> UpdateExpenseAsync(Expense expense)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Verifies the user based on the <see cref="ClaimTypes.NameIdentifier" /> claim.
    /// </summary>
    /// <exception cref="UnauthorizedException">Thrown if the user is not authenticated.</exception>
    /// <exception cref="NotFoundException">Thrown if the user is not found.</exception>
    /// <returns>The user id.</returns>
    private async Task<Guid> GetUserIdFromRequest()
    {
        var nameIdentifierClaim = _httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        UnauthorizedException.ThrowIfNullOrEmpty(nameIdentifierClaim?.Value, "User is not authenticated");
        var userId = Guid.Parse(nameIdentifierClaim!.Value);
        var userExists = await _userRepository.ExistsAsync(u => !u.IsDeleted && u.Id == userId);
        NotFoundException.ThrowIfTrue(!userExists, "User not found");
        return userId;
    }
}
