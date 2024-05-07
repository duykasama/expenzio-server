using Expenzio.Application.ExpenseCategories.Commands.CreateExpenseCategory;
using Expenzio.Application.Expenses.Commands.CreateExpense;
using Expenzio.Application.Expenses.Commands.UpdateExpense;
using Expenzio.Application.Expenses.Commands.UpdateExpenseDetail;
using Expenzio.Domain.Entities;
using Expenzio.Domain.Enums;

namespace Expenzio.Application.FunctionalTests.Expenses.Commands;

using static Testing;

public class UpdateExpenseDetailTests : BaseTestFixture
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

        var listId = await SendAsync(new CreateExpenseCategoryCommand
        {
            Description = "New List"
        });

        var itemId = await SendAsync(new CreateExpenseCommand
        {
            ListId = listId,
            Title = "New Item"
        });

        var command = new UpdateExpenseDetailCommand
        {
            Id = itemId,
            ListId = listId,
            Note = "This is the note.",
            Priority = PriorityLevel.High
        };

        await SendAsync(command);

        var item = await FindAsync<Expense>(itemId);

        item.Should().NotBeNull();
        item!.Id.Should().Be(command.ListId);
        item.LastModifiedBy.Should().NotBeNull();
        item.LastModifiedBy.Should().Be(userId);
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
