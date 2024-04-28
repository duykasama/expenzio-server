using Expenzio.Service.Helpers;

namespace Expenzio.UnitTest.HelperTests;

public class PasswordHelperTests
{
    [Test]
    public void HashPassword_ShouldReturnHashedString_WhenPasswordIsNotNullAndEmpty()
    {
        // Arrange
        var password = "password";

        // Act
        var hashedPassword = PasswordHelper.HashPassword(password);

        // Assert
        Assert.NotNull(hashedPassword);
    }

    [Test]
    public void HashPassword_ShouldThrowArgumentNullException_WhenPasswordIsEmpty()
    {
        // Arrange
        var password = string.Empty;

        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => PasswordHelper.HashPassword(password));
    }

    [Test]
    public void VerifyPassword_ShouldReturnTrue_WhenPasswordIsCorrect()
    {
        // Arrange
        var password = "password";
        var hashedPassword = PasswordHelper.HashPassword(password);
        
        // Act
        var isPasswordCorrect = PasswordHelper.VerifyPassword(password, hashedPassword);

        // Assert
        Assert.That(isPasswordCorrect, Is.True);
    }

    [Test]
    public void VerifyPassword_ShouldReturnFalse_WhenPasswordIsIncorrect()
    {
        // Arrange
        var password = "password";
        var hashedPassword = PasswordHelper.HashPassword("incorrect-password");

        // Act
        var isPasswordCorrect = PasswordHelper.VerifyPassword(password, hashedPassword);

        // Assert
        Assert.That(isPasswordCorrect, Is.False);
    }
}
