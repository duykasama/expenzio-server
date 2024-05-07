namespace Expenzio.Domain.Entities;

public class ExpenseCategory : BaseAuditableEntity<Guid>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string UserId { get; set; } = null!;
    public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}
