using Expenzio.Application.ExpenseCategories.Commands.CreateExpenseCategory;
using Expenzio.Application.Expenses.Commands.CreateExpense;
using Expenzio.Application.Expenses.Commands.DeleteExpense;
using Expenzio.Domain.Entities;

namespace Expenzio.Application.FunctionalTests.Expenses.Commands;

using static Testing;

public class DeleteExpenseTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoItemId()
    {
        var command = new DeleteExpenseCommand(Guid.NewGuid());

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteTodoItem()
    {
        var listId = await SendAsync(new CreateExpenseCategoryCommand
        {
            Description = "New List"
        });

        var itemId = await SendAsync(new CreateExpenseCommand
        {
            ListId = listId,
            Title = "New Item"
        });

        await SendAsync(new DeleteExpenseCommand(itemId));

        var item = await FindAsync<Expense>(itemId);

        item.Should().BeNull();
    }
}
