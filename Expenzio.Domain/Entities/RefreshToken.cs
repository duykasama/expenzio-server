namespace Expenzio.Domain.Entities;

public class RefreshToken
{
    public string Token { get; set; } = null!;
    public Guid UserId { get; set; }
    public ExpenzioUser User { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime ValidUntil { get; set; }
    public bool IsDeleted { get; set; }
}
