using Expenzio.Application.ExpenseCategories.Commands.CreateExpenseCategory;
using Expenzio.Application.ExpenseCategories.Commands.DeleteExpenseCategory;
using Expenzio.Domain.Entities;

namespace Expenzio.Application.FunctionalTests.TodoLists.Commands;

using static Testing;

public class DeleteExpenseCategoryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoListId()
    {
        var command = new DeleteExpenseCategoryCommand(Guid.NewGuid());
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteTodoList()
    {
        var listId = await SendAsync(new CreateExpenseCategoryCommand
        {
            Description = "New List"
        });

        await SendAsync(new DeleteExpenseCategoryCommand(listId));

        var list = await FindAsync<ExpenseCategory>(listId);

        list.Should().BeNull();
    }
}
