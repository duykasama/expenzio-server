using AutoMapper;
using expenzio.DAL.Interfaces;
using Expenzio.Common.Helpers;
using Expenzio.DAL.Interfaces;
using Expenzio.Domain.Entities;
using Expenzio.Domain.Models.Requests.Expense;
using Expenzio.Service;
using Expenzio.Service.Interfaces;
using Expenzio.UnitTest.Helpers;
using Moq;

namespace Expenzio.UnitTest.Services;

[Ignore("Not implemented yet")]
public class AuthServiceTests
{
    private Mock<IExpenseRepository> _expenseRepository = new Mock<IExpenseRepository>();
    private Mock<IExpenseCategoryRepository> _expenseCategoryRepository = new Mock<IExpenseCategoryRepository>();
    private IMapper _mapper = null!;
    private IExpenseService _expenseService = null!;

    [SetUp]
    public void Setup()
    {
        _expenseRepository = new Mock<IExpenseRepository>();
        _expenseCategoryRepository = new Mock<IExpenseCategoryRepository>();
        _mapper = new MapperConfiguration(AutoMapperConfigurationHelper.Configure)
            .CreateMapper();
        _expenseService = new ExpenseService(_expenseRepository.Object, _mapper);
    }

    [Test]
    public async Task AddExpenseAsync_ShouldReturnExpense_WhenRequestIsValid()
    {
        // Arrange
        var request = new CreateExpenseRequest
        {
            Description = "Test Expense",
            Amount = 100,
            MonetaryUnit = "VND",
            CategoryId = Guid.NewGuid(),
        };

        var expense = _mapper.Map<Expense>(request);

        _expenseRepository.Setup(x => x.AddAsync(expense, default));

        // Act
        var result = await _expenseService.AddExpenseAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.That(expense.Id, Is.EqualTo(result.Id));
        Assert.That(expense.Description, Is.EqualTo(result.Description));
        Assert.That(expense.Amount, Is.EqualTo(result.Amount));
        Assert.That(expense.MonetaryUnit, Is.EqualTo(result.MonetaryUnit));
        Assert.Less(expense.CreatedAt - result.CreatedAt, TimeSpan.FromSeconds(1));
    }

    [Test]
    public async Task GetExpensesAsync_ShouldReturnExpenses_WhenDataExists()
    {
        // Arrange
        var expenses = TestDataHelper.ExpensesData();
        _expenseRepository.Setup(x => x.GetAllAsync())
            .ReturnsAsync(expenses.AsQueryable());

        // Act
        var result = await _expenseService.GetExpensesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsNotEmpty(result);
        Assert.That(expenses.Count(), Is.EqualTo(result.Count()));
    }

    [Test]
    public async Task GetExpensesAsync_ShouldReturnEmpty_WhenDataDoesNotExist()
    {
        // Arrange
        var expenses = TestDataHelper.EmptyExpenseData();
        _expenseRepository.Setup(x => x.GetAllAsync())
            .ReturnsAsync(expenses.AsQueryable());

        // Act
        var result = await _expenseService.GetExpensesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsEmpty(result);
        Assert.That(expenses.Count(), Is.EqualTo(result.Count()));
    }
}
