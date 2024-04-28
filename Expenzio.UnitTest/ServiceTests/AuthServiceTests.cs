using System.Linq.Expressions;
using AutoMapper;
using Expenzio.Common.Exceptions;
using Expenzio.Common.Helpers;
using Expenzio.DAL.Interfaces;
using Expenzio.Domain.Entities;
using Expenzio.Domain.Models.Requests.Authentication;
using Expenzio.Service.Implementation;
using Expenzio.Service.Interfaces;
using Moq;

namespace Expenzio.UnitTest.Services;

public class AuthServiceTests
{
    private Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();
    private IMapper _mapper = null!;
    private IAuthService _authService = null!;

    [SetUp]
    public void Setup()
    {
        _userRepository = new Mock<IUserRepository>();
        _mapper = new MapperConfiguration(AutoMapperConfigurationHelper.Configure)
            .CreateMapper();
        _authService = new AuthService(_userRepository.Object, _mapper);
    }

    [Test]
    public async Task RegisterUser_ShouldSucceed_WhenRequestIsValid()
    {
        // Arrange
        var request = new RegisterRequest
        {
            Email = "test.user@expenzio.com",
            Phone = "0987654321",
            Password = "$tr0ngP@$$w0rd123@@",
            FirstName = "Test",
            LastName = "User",
        };
        
        // Act
        var apiResponse = await _authService.RegisterAsync(request, default);

        // Assert
        Assert.NotNull(apiResponse);
        Assert.That(apiResponse.Success, Is.True);
        Assert.That(apiResponse.StatusCode, Is.EqualTo(201));
    }

    [Test]
    public void RegisterUser_ShouldThrowBadRequest_WhenEmailIsMissing()
    {
        // Arrange
        var request = new RegisterRequest
        {
            Phone = "0987654321",
            Password = "$tr0ngP@$$w0rd123@@",
            FirstName = "Test",
            LastName = "User",
        };
        
        // Act
        // Assert
        Assert.ThrowsAsync<BadRequestException>(async () => await _authService.RegisterAsync(request, default));
    }

    [Test]
    public void RegisterUser_ShouldThrowBadRequest_WhenEmailIsIncorrect()
    {
        // Arrange
        var request = new RegisterRequest
        {
            Email = "test.user",
            Phone = "0987654321",
            Password = "$tr0ngP@$$w0rd123@@",
            FirstName = "Test",
            LastName = "User",
        };
        
        // Act
        // Assert
        Assert.ThrowsAsync<BadRequestException>(async () => await _authService.RegisterAsync(request, default));
    }

    [Test]
    public void RegisterUser_ShouldThrowBadRequest_WhenPasswordIsMissing()
    {
        // Arrange
        var request = new RegisterRequest
        {
            Email = "test.user@expenzio.com",
            Phone = "0987654321",
            FirstName = "Test",
            LastName = "User",
        };
        
        // Act
        // Assert
        Assert.ThrowsAsync<BadRequestException>(async () => await _authService.RegisterAsync(request, default));
    }

    [Test]
    public void RegisterUser_ShouldThrowBadRequest_WhenPasswordIsTooSimple()
    {
        // Arrange
        var request = new RegisterRequest
        {
            Email = "test.user@expenzio.com",
            Password = "123",
            Phone = "0987654321",
            FirstName = "Test",
            LastName = "User",
        };
        
        // Act
        // Assert
        Assert.ThrowsAsync<BadRequestException>(async () => await _authService.RegisterAsync(request, default));
    }

    [Test]
    public void RegisterUser_ShouldThrowBadRequest_WhenPhoneNumberIsMissing()
    {
        // Arrange
        var request = new RegisterRequest
        {
            Email = "test.user@expenzio.com",
            Password = "$tr0ngP@$$w0rd123@@",
            FirstName = "Test",
            LastName = "User",
        };
        
        // Act
        // Assert
        Assert.ThrowsAsync<BadRequestException>(async () => await _authService.RegisterAsync(request, default));
    }

    [Test]
    public void RegisterUser_ShouldThrowBadRequest_WhenFirstNameIsMissing()
    {
        // Arrange
        var request = new RegisterRequest
        {
            Email = "test.user@expenzio.com",
            Phone = "0987654321",
            Password = "$tr0ngP@$$w0rd123@@",
            LastName = "User",
        };
        
        // Act
        // Assert
        Assert.ThrowsAsync<BadRequestException>(async () => await _authService.RegisterAsync(request, default));
    }

    [Test]
    public void RegisterUser_ShouldThrowBadRequest_WhenLastNameIsMissing()
    {
        // Arrange
        var request = new RegisterRequest
        {
            Email = "test.user@expenzio.com",
            Phone = "0987654321",
            Password = "$tr0ngP@$$w0rd123@@",
            FirstName = "Test",
        };
        
        // Act
        // Assert
        Assert.ThrowsAsync<BadRequestException>(async () => await _authService.RegisterAsync(request, default));
    }

    [Test]
    public void RegisterUser_ShouldReturnFailureResponse_WhenEmailAlreadyExists()
    {
        // Arrange
        var request = new RegisterRequest
        {
            Email = "already.exists@expenzio.com",
            Password = "$tr0ngP@$$w0rd123@@",
            Phone = "0987654321",
            FirstName = "Test",
            LastName = "User",
        };
        _userRepository.Setup(u => u.ExistsAsync(It.IsAny<Expression<Func<ExpenzioUser, bool>>>()))
            .ReturnsAsync(true);
        
        // Act
        // Assert
        Assert.ThrowsAsync<ConflictException>(async () => await _authService.RegisterAsync(request, default));
    }
}
