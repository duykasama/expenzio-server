using Expenzio.Domain;

namespace Expenzio.Api;

public class Query {
		public Expense GetExpense() {
				return new Expense {
						Id = Guid.NewGuid(),
						Description = "Expense description",
						Amount = 100,
						MonetaryUnit = "USD",
						Date = DateTime.Now,
						CategoryId = Guid.NewGuid()
				};
		}
}
