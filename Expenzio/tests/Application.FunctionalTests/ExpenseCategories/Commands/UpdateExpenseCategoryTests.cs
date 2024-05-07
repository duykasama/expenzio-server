using Expenzio.Application.Common.Exceptions;
using Expenzio.Application.ExpenseCategories.Commands.CreateExpenseCategory;
using Expenzio.Application.ExpenseCategories.Commands.UpdateExpenseCategory;
using Expenzio.Domain.Entities;

namespace Expenzio.Application.FunctionalTests.TodoLists.Commands;

using static Testing;

public class UpdateExpenseCategoryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoListId()
    {
        var command = new UpdateExpenseCategoryCommand { Id = Guid.NewGuid(), Title = "New Title" };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldRequireUniqueTitle()
    {
        var listId = await SendAsync(new CreateExpenseCategoryCommand
        {
            Description = "New List"
        });

        await SendAsync(new CreateExpenseCategoryCommand 
        {
            Description = "Other List"
        });

        var command = new CreateExpenseCategoryCommand 
        {
            Description = "Other List"
        };

        (await FluentActions.Invoking(() =>
            SendAsync(command))
                .Should().ThrowAsync<ValidationException>().Where(ex => ex.Errors.ContainsKey("Title")))
                .And.Errors["Title"].Should().Contain("'Title' must be unique.");
    }

    [Test]
    public async Task ShouldUpdateTodoList()
    {
        var userId = await RunAsDefaultUserAsync();

        var listId = await SendAsync(new CreateExpenseCategoryCommand
        {
            Description = "New List"
        });

        var command = new UpdateExpenseCategoryCommand
        {
            Id = listId,
            Title = "Updated List Title"
        };

        await SendAsync(command);

        var list = await FindAsync<ExpenseCategory>(listId);

        list.Should().NotBeNull();
        list!.Description.Should().Be(command.Title);
        list.LastModifiedBy.Should().NotBeNull();
        list.LastModifiedBy.Should().Be(userId);
        list.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
