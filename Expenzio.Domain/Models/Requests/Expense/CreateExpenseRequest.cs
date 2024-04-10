namespace Expenzio.Domain.Models.Requests.Expense;

public class CreateExpenseRequest {
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public string MonetaryUnit { get; set; } = null!;
    public Guid CategoryId { get; set; }
}
