using Expenzio.Application.ExpenseCategories.Commands.CreateExpenseCategory;
using Expenzio.Application.ExpenseCategories.Commands.DeleteExpenseCategory;
using Expenzio.Application.ExpenseCategories.Commands.UpdateExpenseCategory;
// using Expenzio.Application.ExpenseCategories.Queries.GetExpenses;

namespace Expenzio.Web.Endpoints;

public class ExpenseCategories : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            // .MapGet(GetExpenseCategories)
            .MapPost(CreateExpenseCategory)
            .MapPut(UpdateExpenseCategory, "{id}")
            .MapDelete(DeleteExpenseCategory, "{id}");
    }

    // public Task<TodosVm> GetExpenseCategories(ISender sender)
    // {
    //     return  sender.Send(new GetExpensesQuery());
    // }

    public Task<Guid> CreateExpenseCategory(ISender sender, CreateExpenseCategoryCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> UpdateExpenseCategory(ISender sender, Guid id, UpdateExpenseCategoryCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteExpenseCategory(ISender sender, Guid id)
    {
        await sender.Send(new DeleteExpenseCategoryCommand(id));
        return Results.NoContent();
    }
}
