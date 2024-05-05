using System.Linq.Expressions;
using System.Security.Claims;
using AutoMapper;
using expenzio.DAL.Interfaces;
using Expenzio.Common.Exceptions;
using Expenzio.Common.Helpers;
using Expenzio.DAL.Interfaces;
using Expenzio.Domain.Entities;
using Expenzio.Domain.Models.Requests;
using Expenzio.Domain.Models.Responses;
using Expenzio.Service.Implementation;
using Expenzio.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Moq;

namespace ExpenseCategoryServiceTests;

public class ExpenseCategoryServiceTests
{
    private readonly Mock<IExpenseCategoryRepository> _expenseCategoryRepositoryMock = new();
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock = new();
    private readonly IExpenseCategoryService _expenseCategoryService;
    private readonly IMapper _mapper = null!;

    public ExpenseCategoryServiceTests()
    {
        _mapper = new MapperConfiguration(AutoMapperConfigurationHelper.Configure).CreateMapper();
        _expenseCategoryService = new ExpenseCategoryService(
            _expenseCategoryRepositoryMock.Object,
            _userRepositoryMock.Object,
            _httpContextAccessorMock.Object,
            _mapper
        );
    }

    [Test]
    public async Task CreateExpenseCategoryAsync_ShouldReturnCreated_WhenRequestIsValid()
    {
        // Arrange
        var request = new CreateExpenseCategoryRequest { Name = "Category 1" };
        // Ensure adding expense category is successful
        _expenseCategoryRepositoryMock.Setup(x => x.AddAsync(It.IsAny<ExpenseCategory>(), default));
        // Ensure user id exists in claims
        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(x => x.User.FindFirst(ClaimTypes.NameIdentifier))
            .Returns(new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()));
        _httpContextAccessorMock.Setup(x => x.HttpContext)
            .Returns(httpContextMock.Object);
        // Ensure user exists in the database
        _userRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<Expression<Func<ExpenzioUser, bool>>>()))
            .ReturnsAsync(true);
        var expenseCategoryService = new ExpenseCategoryService(
            _expenseCategoryRepositoryMock.Object,
            _userRepositoryMock.Object,
            _httpContextAccessorMock.Object,
            _mapper
        );

        // Act
        var result = await expenseCategoryService.CreateExpenseCategoryAsync(request, default);

        // Assert
        Assert.NotNull(result);
        Assert.That(result, Is.InstanceOf<ApiResponse>());
        Assert.That(result.Success, Is.True);
        Assert.NotNull(result.Data);
        Assert.That(result.Data, Is.InstanceOf<CreatedExpenseCategoryResponse>());
        Assert.AreEqual(StatusCodes.Status201Created, result.StatusCode);
    }

    [Test]
    public void CreateExpenseCategoryAsync_ShouldThrowNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var request = new CreateExpenseCategoryRequest { Name = "Category 1" };
        // Ensure user id exists in claims
        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(x => x.User.FindFirst(ClaimTypes.NameIdentifier))
            .Returns(new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()));
        _httpContextAccessorMock.Setup(x => x.HttpContext)
            .Returns(httpContextMock.Object);
        // Ensure user does not exist in the database
        _userRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<Expression<Func<ExpenzioUser, bool>>>()))
            .ReturnsAsync(false);
        var expenseCategoryService = new ExpenseCategoryService(
            _expenseCategoryRepositoryMock.Object,
            _userRepositoryMock.Object,
            _httpContextAccessorMock.Object,
            _mapper
        );

        // Act
        // Assert
        Assert.ThrowsAsync<NotFoundException>(() => expenseCategoryService.CreateExpenseCategoryAsync(request, default));
    }

    [Test]
    public void CreateExpenseCategoryAsync_ShouldThrowUnauthorized_WhenUserIdDoesNotExistsInClaimPrincipals()
    {
        // Arrange
        var request = new CreateExpenseCategoryRequest { Name = "Category 1" };
        // Ensure user id does not exist in claims
        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(x => x.User.FindFirst(ClaimTypes.NameIdentifier))
            .Returns(It.IsAny<Claim>());
        _httpContextAccessorMock.Setup(x => x.HttpContext)
            .Returns(httpContextMock.Object);
        var expenseCategoryService = new ExpenseCategoryService(
            _expenseCategoryRepositoryMock.Object,
            _userRepositoryMock.Object,
            _httpContextAccessorMock.Object,
            _mapper
        );

        // Act
        // Assert
        Assert.ThrowsAsync<UnauthorizedException>(() => expenseCategoryService.CreateExpenseCategoryAsync(request, default));
    }

    [Test]
    public void CreateExpenseCategoryAsync_ShouldThrowBadRequest_WhenCategoryNameIsMissing()
    {
        // Arrange
        // Invalid request
        var request = new CreateExpenseCategoryRequest();
        // Ensure adding expense category is successful
        _expenseCategoryRepositoryMock.Setup(x => x.AddAsync(It.IsAny<ExpenseCategory>(), default));
        // Ensure user id exists in claims
        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(x => x.User.FindFirst(ClaimTypes.NameIdentifier))
            .Returns(new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()));
        _httpContextAccessorMock.Setup(x => x.HttpContext)
            .Returns(httpContextMock.Object);
        // Ensure user exists in the database
        _userRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<Expression<Func<ExpenzioUser, bool>>>()))
            .ReturnsAsync(true);
        var expenseCategoryService = new ExpenseCategoryService(
            _expenseCategoryRepositoryMock.Object,
            _userRepositoryMock.Object,
            _httpContextAccessorMock.Object,
            _mapper
        );

        // Act
        // Assert
        Assert.ThrowsAsync<BadRequestException>(() => expenseCategoryService.CreateExpenseCategoryAsync(request, default));
    }
}
