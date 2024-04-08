namespace Expenzio.Domain;

public class Expense {
		public Guid Id { get; set; }
		public string Description { get; set; }
		public decimal Amount { get; set; }
		public string MonetaryUnit { get; set; }
		public DateTime Date { get; set; }
		public Guid CategoryId { get; set; }
		public Category Category { get; set; }
}
