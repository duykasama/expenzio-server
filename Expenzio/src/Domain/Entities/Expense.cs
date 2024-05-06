namespace Expenzio.Domain.Entities;

public class Expense : BaseAuditableEntity
{
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public string MonetaryUnit { get; set; } = null!;
    public bool IsDeleted { get; set; }
    public Guid CategoryId { get; set; }
    public Guid UserId { get; set; }
    public ExpenzioUser User { get; set; } = null!;
    public ExpenseCategory Category { get; set; } = null!;
}
