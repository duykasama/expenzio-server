using System.Linq.Expressions;
using AutoMapper;
using Expenzio.Common.Exceptions;
using Expenzio.Common.Helpers;
using Expenzio.DAL.Interfaces;
using Expenzio.Domain.Entities;
using Expenzio.Domain.Models.Requests.Authentication;
using Expenzio.Service.Helpers;
using Expenzio.Service.Implementation;
using Expenzio.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Expenzio.UnitTest.ServiceTests;

public class AuthServiceTests
{
    private Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();
    private Mock<IJwtService> _jwtService = new Mock<IJwtService>();
    private IAuthService _authService = null!;

    [SetUp]
    public void Setup()
    {
        _userRepository = new Mock<IUserRepository>();
        _jwtService = new Mock<IJwtService>();
        var httpContextAccessor = new Mock<IHttpContextAccessor>();
        var mapper = new MapperConfiguration(AutoMapperConfigurationHelper.Configure)
            .CreateMapper();
        _authService = new AuthService(
            _userRepository.Object,
            mapper,
            _jwtService.Object,
            httpContextAccessor.Object
        );
    }

    [Test]
    public async Task RegisterUser_ShouldReturnSuccessResponse_WhenRequestIsValid()
    {
        // Arrange
        var request = new RegisterRequest
        {
            Email = "test.user@expenzio.com",
            Username = "test.user",
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
    public void RegisterUser_ShouldThrowBadRequest_WhenUsernameIsMissing()
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
        var request = new Domain.Models.Requests.Authentication.RegisterRequest
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
            Username = "already.exists",
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

    [Test]
    public async Task LoginUser_ShouldReturnTokenResponse_WhenRequestIsValid()
    {
        // Arrange
        var request = new LoginRequest
        {
            Email = "test.user@expenzio.com",
            Password = "$tr0ngP@$$w0rd123@@",
        };
        _userRepository.Setup(u => u.GetAsync(It.IsAny<Expression<Func<ExpenzioUser, bool>>>(), default))
            .ReturnsAsync(new ExpenzioUser
            {
                Email = "test.user@expenzio.com",
                Password = PasswordHelper.HashPassword("$tr0ngP@$$w0rd123@@"),
                Phone = "0987654321",
                FirstName = "Test",
                LastName = "User",
            });
        _jwtService.Setup(j => j.GenerateAccessToken(It.IsAny<ExpenzioUser>(), It.IsAny<string[]>()))
            .Returns("access-token");
        _jwtService.Setup(j => j.GenerateRefreshToken(It.IsAny<Guid>()))
            .Returns("refresh-token");

        // Act
        var apiResponse = await _authService.LoginAsync(request, default);

        // Assert
        Assert.NotNull(apiResponse);
        Assert.That(apiResponse.Success, Is.True);
        Assert.That(apiResponse.StatusCode, Is.EqualTo(200));
        Assert.NotNull(apiResponse.Data);
        Assert.That(apiResponse.Data.AccessToken, Is.Not.Null.And.Not.Empty);
        Assert.That(apiResponse.Data.RefreshToken, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void LoginUser_ShouldThrowNotFound_WhenEmailDoesNotExist()
    {
        // Arrange
        var request = new LoginRequest
        {
            Email = "not.found@expenzio.com",
            Password = "$tr0ngP@$$w0rd123@@",
        };

        // Act
        // Assert
        Assert.ThrowsAsync<NotFoundException>(async () => await _authService.LoginAsync(request, default));
    }

    [Test]
    public void LoginUser_ShouldThrowBadRequest_WhenEmailIsMissing()
    {
        // Arrange
        var request = new LoginRequest
        {
            Password = "$tr0ngP@$$w0rd123@@",
        };

        // Act
        // Assert
        Assert.ThrowsAsync<BadRequestException>(async () => await _authService.LoginAsync(request, default));
    }

    [Test]
    public void LoginUser_ShouldThrowBadRequest_WhenPasswordIsMissing()
    {
        // Arrange
        var request = new LoginRequest
        {
            Email = "not.found@expenzio.com",
        };

        // Act
        // Assert
        Assert.ThrowsAsync<BadRequestException>(async () => await _authService.LoginAsync(request, default));
    }

    [Test]
    public void LoginUser_ShouldThrowUnauthorized_WhenPasswordIsIncorrect()
    {
        // Arrange
        var request = new LoginRequest
        {
            Email = "test.user@expenzio.com",
            Password = "$tr0ngP@$$w0rd123@@",
        };
        _userRepository.Setup(u => u.GetAsync(It.IsAny<Expression<Func<ExpenzioUser, bool>>>(), default))
            .ReturnsAsync(new ExpenzioUser
            {
                Email = "test.user@expenzio.com",
                Password = "wrong-password",
                Phone = "0987654321",
                FirstName = "Test",
                LastName = "User",
            });
        
        // Act
        // Assert
        Assert.ThrowsAsync<UnauthorizedException>(async () => await _authService.LoginAsync(request, default));
    }
}
