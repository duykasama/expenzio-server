namespace Expenzio.Domain.Entities;

public class ExpenzioUser
{
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string FristName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}
