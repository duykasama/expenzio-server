using Expenzio.Application.Common.Exceptions;
using Expenzio.Application.ExpenseCategories.Commands.CreateExpenseCategory;
using Expenzio.Application.Expenses.Commands.CreateExpense;
using Expenzio.Domain.Entities;

namespace Expenzio.Application.FunctionalTests.Expenses.Commands;

using static Testing;

public class CreateExpenseTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateExpenseCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateTodoItem()
    {
        var userId = await RunAsDefaultUserAsync();

        var listId = await SendAsync(new CreateExpenseCategoryCommand
        {
        });

        var command = new CreateExpenseCommand
        {
            ListId = listId,
            Title = "Tasks"
        };

        var itemId = await SendAsync(command);

        var item = await FindAsync<Expense>(itemId);

        item.Should().NotBeNull();
        item!.Id.Should().Be(command.ListId);
        item.Description.Should().Be(command.Title);
        item.CreatedBy.Should().Be(userId);
        item.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        item.LastModifiedBy.Should().Be(userId);
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
