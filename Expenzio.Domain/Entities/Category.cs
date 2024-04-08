namespace Expenzio.Domain;

public class Category {
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public ICollection<Expense> Expenses { get; set; }
}
