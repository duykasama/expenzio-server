namespace Expenzio.Domain.Entities;

public class Category {
		public Guid Id { get; set; }
		public string Name { get; set; } = null!;
		public string? Description { get; set; }
		public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}
