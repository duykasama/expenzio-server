namespace Expenzio.Domain.Entities;

public class ExpenseCategory : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Guid UserId { get; set; }
    public ExpenzioUser User { get; set; } = null!;
    public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}
