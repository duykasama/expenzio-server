using Expenzio.Domain.Entities;
using Expenzio.Service.Implementation;
using Expenzio.Service.Interfaces;
using Expenzio.Service.Settings;

namespace Expenzio.UnitTest.ServicesTests;

public class TokenHelperTests
{
    private IJwtService _jwtService = null!;
    private JwtSettings _jwtSettings = null!;

    [SetUp]
    public void Setup()
    {
        _jwtSettings = new JwtSettings()
        {
            Issuer = "Expenzio",
            Audience = "Expenzio",
            SigningKey = "ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ",
            AccessTokenLifetimeInMinutes = 30,
            RefreshTokenLifetimeInMinutes = 60,
        };
        _jwtService = new JwtService(_jwtSettings);
    }

    [Test]
    public void GenerateAccessToken_ShouldReturnString_WhenCalled()
    {
        // Arrange
        var user = new ExpenzioUser()
        {
            Id = Guid.NewGuid(),
            Email = "test.user@expenzio.com",
            Username = "Test User",
        };
        var roles = new List<string> { "User" };
        
        // Act
        var accessToken = _jwtService.GenerateAccessToken(user, roles);

        // Assert
        Assert.That(accessToken, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void GenerateRefreshToken_ShouldReturnString_WhenCalled()
    {
        // Arrange
        var userId = Guid.NewGuid();
        
        // Act
        var refreshToken = _jwtService.GenerateRefreshToken(userId);

        // Assert
        Assert.That(refreshToken, Is.Not.Null.And.Not.Empty);
    }
}
