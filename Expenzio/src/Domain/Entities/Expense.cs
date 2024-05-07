namespace Expenzio.Domain.Entities;

public class Expense : BaseAuditableEntity<Guid>
{
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public string MonetaryUnit { get; set; } = null!;
    public bool IsDeleted { get; set; }
    public string UserId { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public ExpenseCategory Category { get; set; } = null!;
}
