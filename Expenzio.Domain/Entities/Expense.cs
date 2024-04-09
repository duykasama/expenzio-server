namespace Expenzio.Domain.Entities;

public class Expense {
		public Guid Id { get; set; }
		public string? Description { get; set; }
		public decimal Amount { get; set; }
		public string MonetaryUnit { get; set; } = null!;
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public bool IsDeleted { get; set; }
		public Guid CategoryId { get; set; }
		public ExpenseCategory Category { get; set; } = null!;
}
