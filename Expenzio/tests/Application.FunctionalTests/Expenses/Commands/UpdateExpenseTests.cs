using Expenzio.Application.Expenses.Commands.CreateExpense;
using Expenzio.Application.Expenses.Commands.UpdateExpense;
using Expenzio.Domain.Entities;

namespace Expenzio.Application.FunctionalTests.Expenses.Commands;

using static Testing;

public class UpdateExpenseTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoItemId()
    {
        var command = new UpdateExpenseCommand { Id = Guid.NewGuid(), Title = "New Title" };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateTodoItem()
    {
        var userId = await RunAsDefaultUserAsync();

        var listId = await SendAsync(new CreateExpenseCommand
        {
            Title = "New List"
        });

        var itemId = await SendAsync(new CreateExpenseCommand
        {
            ListId = listId,
            Title = "New Item"
        });

        var command = new UpdateExpenseCommand
        {
            Id = itemId,
            Title = "Updated Item Title"
        };

        await SendAsync(command);

        var item = await FindAsync<Expense>(itemId);

        item.Should().NotBeNull();
        item!.Description.Should().Be(command.Title);
        item.LastModifiedBy.Should().NotBeNull();
        item.LastModifiedBy.Should().Be(userId);
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
