using Expenzio.Application.Common.Exceptions;
using Expenzio.Application.ExpenseCategories.Commands.CreateExpenseCategory;
using Expenzio.Domain.Entities;

namespace Expenzio.Application.FunctionalTests.TodoLists.Commands;

using static Testing;

public class CreateExpenseCategoryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateExpenseCategoryCommand();
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRequireUniqueTitle()
    {
        await SendAsync(new CreateExpenseCategoryCommand
        {
            Description = "Shopping"
        });

        var command = new CreateExpenseCategoryCommand
        {
            Description = "Shopping"
        };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateTodoList()
    {
        var userId = await RunAsDefaultUserAsync();

        var command = new CreateExpenseCategoryCommand
        {
            Description = "Tasks"
        };

        var id = await SendAsync(command);

        var list = await FindAsync<Expense>(id);

        list.Should().NotBeNull();
        list!.Description.Should().Be(command.Description);
        list.CreatedBy.Should().Be(userId);
        list.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
