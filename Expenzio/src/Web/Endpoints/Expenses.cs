using Expenzio.Application.Common.Models;
using Expenzio.Application.Expenses.Commands.CreateExpense;
using Expenzio.Application.Expenses.Commands.DeleteExpense;
using Expenzio.Application.Expenses.Commands.UpdateExpense;
using Expenzio.Application.Expenses.Commands.UpdateExpenseDetail;
using Expenzio.Application.Expenses.Queries.GetExpensesWithPagination;

namespace Expenzio.Web.Endpoints;

public class Expenses : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetExpensesWithPagination)
            .MapPost(CreateExpense)
            .MapPut(UpdateExpense, "{id}")
            .MapPut(UpdateExpenseDetail, "UpdateDetail/{id}")
            .MapDelete(DeleteExpense, "{id}");
    }

    public Task<PaginatedList<ExpenseBriefDto>> GetExpensesWithPagination(ISender sender, [AsParameters] GetExpensesWithPaginationQuery query)
    {
        return sender.Send(query);
    }

    public Task<int> CreateExpense(ISender sender, CreateExpenseCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> UpdateExpense(ISender sender, int id, UpdateExpenseCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> UpdateExpenseDetail(ISender sender, int id, UpdateExpenseDetailCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteExpense(ISender sender, int id)
    {
        await sender.Send(new DeleteExpenseCommand(id));
        return Results.NoContent();
    }
}
