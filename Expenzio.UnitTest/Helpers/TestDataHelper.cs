using Expenzio.Domain.Entities;

namespace Expenzio.UnitTest.Helpers;

public static class TestDataHelper 
{
    public static IEnumerable<Expense> ExpensesData()
    {
        return new List<Expense>
        {
            new Expense
            {
                Id = Guid.NewGuid(),
                Description = "Test Expense",
                Amount = 100,
                MonetaryUnit = "VND",
                CreatedAt = DateTime.Now
            },
            new Expense
            {
                Id = Guid.NewGuid(),
                Description = "Test Expense",
                Amount = 200,
                MonetaryUnit = "VND",
                CreatedAt = DateTime.Now
            }
        };
    }

    public static IEnumerable<Expense> EmptyExpenseData()
    {
        return new List<Expense>();
    }

    public static IQueryable<ExpenseCategory> ExpenseCategoriesData()
    {
        return new List<ExpenseCategory>
        {
            new ExpenseCategory 
            {
                Id = Guid.NewGuid(),
                Description = "Test Expense",
                Name = "Category 1",
            },
            new ExpenseCategory 
            {
                Id = Guid.NewGuid(),
                Description = "Test Expense",
                Name = "Category 2",
            }
        }
        .AsQueryable();
    }
}
