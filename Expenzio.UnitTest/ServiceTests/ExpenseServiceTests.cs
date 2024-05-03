using System.Linq.Expressions;
using System.Security.Claims;
using AutoMapper;
using expenzio.DAL.Interfaces;
using Expenzio.Common.Exceptions;
using Expenzio.Common.Helpers;
using Expenzio.DAL.Interfaces;
using Expenzio.Domain.Entities;
using Expenzio.Domain.Models.Requests.Expense;
using Expenzio.Service;
using Expenzio.Service.Interfaces;
using Expenzio.UnitTest.Helpers;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Expenzio.UnitTest.ServiceTests;

public class ExpenseServiceTests
{
    private Mock<IExpenseRepository> _expenseRepository = new Mock<IExpenseRepository>();
    private Mock<IExpenseCategoryRepository> _expenseCategoryRepository = new Mock<IExpenseCategoryRepository>();
    private Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();
    private Mock<IHttpContextAccessor> _httpContextAccessor = new Mock<IHttpContextAccessor>();
    private IExpenseService _expenseService = null!;
    private IMapper _mapper = null!;

    [SetUp]
    public void Setup()
    {
        _expenseRepository = new Mock<IExpenseRepository>();
        _expenseCategoryRepository = new Mock<IExpenseCategoryRepository>();
        _httpContextAccessor = new Mock<IHttpContextAccessor>();
        _mapper = new MapperConfiguration(AutoMapperConfigurationHelper.Configure)
            .CreateMapper();
        _expenseService = new ExpenseService(
            _expenseRepository.Object,
            _mapper,
            _httpContextAccessor.Object,
            _userRepository.Object
        );
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
        var expense = new Expense()
        {
            Description = request.Description,
            Amount = request.Amount,
            MonetaryUnit = request.MonetaryUnit,
            CategoryId = request.CategoryId
        };

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

    [Test]
    public async Task GetWeeklyExpensesAsync_ShouldReturnExpenses_WhenRequestIsValid()
    {
        // Arrange
        var validClaim = new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString());
        var httpContext = new Mock<HttpContext>();
        // Ensure claim exists
        httpContext.Setup(c => c.User.FindFirst(ClaimTypes.NameIdentifier))
            .Returns(validClaim);
        _httpContextAccessor.Setup(a => a.HttpContext)
            .Returns(httpContext.Object);
        // Ensure user exists
        _userRepository.Setup(uR => uR.ExistsAsync(It.IsAny<Expression<Func<ExpenzioUser, bool>>>()))
            .ReturnsAsync(true);
        // Mock data
        _expenseRepository.Setup(eR => eR.FindAsync(It.IsAny<Expression<Func<Expense, bool>>>()))
            .ReturnsAsync(TestDataHelper.ExpensesData().AsQueryable());
        _expenseService = new ExpenseService(
            _expenseRepository.Object,
            _mapper,
            _httpContextAccessor.Object,
            _userRepository.Object
        );

        // Act
        var result = await _expenseService.GetWeeklyExpensesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsNotEmpty(result);
    }

    [Test]
    public void GetWeeklyExpensesAsync_ShouldThrowNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var validClaim = new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString());
        var httpContext = new Mock<HttpContext>();
        // Ensure claim exists
        httpContext.Setup(c => c.User.FindFirst(ClaimTypes.NameIdentifier))
            .Returns(validClaim);
        _httpContextAccessor.Setup(a => a.HttpContext)
            .Returns(httpContext.Object);
        // Ensure user does not exist
        _userRepository.Setup(uR => uR.ExistsAsync(It.IsAny<Expression<Func<ExpenzioUser, bool>>>()))
            .ReturnsAsync(false);
        _expenseService = new ExpenseService(
            _expenseRepository.Object,
            _mapper,
            _httpContextAccessor.Object,
            _userRepository.Object
        );

        // Act
        // Assert
        Assert.ThrowsAsync<NotFoundException>(async () => await _expenseService.GetWeeklyExpensesAsync());
    }

    [Test]
    public void GetWeeklyExpensesAsync_ShouldThrowUnauthorized_WhenUserIdDoesNotExistsInClaimPrincipals()
    {
        // Arrange
        var httpContext = new Mock<HttpContext>();
        // Ensure claim does not exist
        httpContext.Setup(c => c.User.FindFirst(ClaimTypes.NameIdentifier))
            .Returns(It.IsAny<Claim>());
        _httpContextAccessor.Setup(a => a.HttpContext)
            .Returns(httpContext.Object);
        _expenseService = new ExpenseService(
            _expenseRepository.Object,
            _mapper,
            _httpContextAccessor.Object,
            _userRepository.Object
        );

        // Act
        // Assert
        Assert.ThrowsAsync<UnauthorizedException>(async () => await _expenseService.GetWeeklyExpensesAsync());
    }
}
