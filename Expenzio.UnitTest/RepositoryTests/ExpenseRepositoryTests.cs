using AutoMapper;
using Expenzio.Common.Helpers;
using Expenzio.DAL.Data;
using Expenzio.DAL.Implementations;
using Expenzio.DAL.Interfaces;
using Expenzio.Domain.Entities;
using Expenzio.Domain.Models.Requests.Expense;
using Expenzio.UnitTest.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Expenzio.UnitTest.RepositoryTests;

class ExpenseRepositoryTests 
{
    private ExpenzioDbContext _context = null!;
    private IExpenseRepository _repo = null!;
    private IMapper _mapper = null!;

    [SetUp]
    public async Task Setup()
    {
        var options = new DbContextOptionsBuilder<ExpenzioDbContext>()
            .UseInMemoryDatabase(databaseName: "Expenzio")
            .Options;
        _context = new ExpenzioDbContext(options);
        await _context.Set<Expense>().AddRangeAsync(TestDataHelper.ExpensesData());
        await _context.Set<ExpenseCategory>().AddRangeAsync(TestDataHelper.ExpenseCategoriesData());
        await _context.SaveChangesAsync();
        _repo = new ExpenseRepository(_context);
        var config = new MapperConfiguration(AutoMapperConfigurationHelper.Configure);
        _mapper = config.CreateMapper();
    }

    [TearDown]
    public async Task TearDown()
    {
        await _context.DisposeAsync();
    }

    [Test]
    public async Task GetExpenseById_ReturnsExpense()
    {
        // Arrange
        var expenseId = Guid.NewGuid();
        await _context.Set<Expense>().AddAsync(
            new ()
            {
                Id = expenseId,
                Description = "Test Expense",
                Amount = 100,
                MonetaryUnit = "VND",
                CreatedAt = DateTime.Now
            }
        );
        await _context.SaveChangesAsync();
        var repo = new ExpenseRepository(_context);

        // Act
        var result = await repo.GetAsync(e => e.Id == expenseId);

        // Assert
        Assert.NotNull(result);
    }

    [Test]
    public async Task GetExpenseById_ReturnsNull()
    {
        // Arrange

        // Act
        var result = await _repo.GetAsync(e => e.Id == Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Test]
    public async Task GetAllExpenses_ReturnsExpenses()
    {
        // Arrange
        
        // Act
        var result = (await _repo.GetAllAsync()).ToList();

        // Assert
        Assert.NotNull(result);
        Assert.IsNotEmpty(result);
    }

    [Test]
    public async Task GetAllExpenses_ReturnsEmpty()
    {
        // Arrange
        _context.Set<Expense>().RemoveRange(await _context.Set<Expense>().ToListAsync());
        await _context.SaveChangesAsync();

        // Act
        var result = (await _repo.GetAllAsync()).ToList();

        // Assert
        Assert.NotNull(result);
        Assert.IsEmpty(result);
    }

    [Test]
    public async Task CreateExpense_Success()
    {
        // Arange
        var request = new CreateExpenseRequest()
        {
            Description = string.Empty,
                        Amount = 100_000,
                        MonetaryUnit = "VND",
        };
        var originalExpensesCount = (await _repo.GetAllAsync()).Count();

        // Act
        var expense = _mapper.Map<Expense>(request);
        await _repo.AddAsync(expense);
        var newExpensesCount = (await _repo.GetAllAsync()).Count();

        // Assert 
        Assert.That(originalExpensesCount + 1, Is.EqualTo(newExpensesCount));
    }

    [Test]
    public async Task CreateExpense_MissingMonetaryUnit()
    {
        // Arange
        var invalidRequest = new CreateExpenseRequest()
        {
            Description = string.Empty,
                        Amount = 100_000,
        };
        var originalExpensesCount = (await _repo.GetAllAsync()).Count();

        // Act
        // Assert 
        var expense = _mapper.Map<Expense>(invalidRequest);
        Assert.ThrowsAsync(typeof(DbUpdateException), async () => await _repo.AddAsync(expense));
        var newExpensesCount = (await _repo.GetAllAsync()).Count();
        Assert.That(originalExpensesCount, Is.EqualTo(newExpensesCount));
    }
}
