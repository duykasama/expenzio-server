namespace Expenzio.Domain.Models.Requests.Authentication;

public class LoginRequest
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
