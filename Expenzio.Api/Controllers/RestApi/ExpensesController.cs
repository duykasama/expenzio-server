using Expenzio.Domain.Models.Requests.Expense;
using Expenzio.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Expenzio.Api.Controllers.RestApi;

public class ExpensesController : BaseApiController 
{
    private readonly IExpenseService _expenseService;
    public ExpensesController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    [HttpPost]
    public async Task<IActionResult> AddExpense([FromBody] CreateExpenseRequest request, CancellationToken cancellationToken = default)
    {
        return await ExecuteAsync(
            async () => await _expenseService.AddExpenseAsync(request, cancellationToken)
        );
    }
}
