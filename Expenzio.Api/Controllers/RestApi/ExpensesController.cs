using Expenzio.Domain.Models.Requests.Expense;
using Expenzio.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Expenzio.Api.Controllers.RestApi;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase 
{
    private readonly IExpenseService _expenseService;
    public ExpensesController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    [HttpPost]
    public async Task<IActionResult> AddExpense([FromBody] CreateExpenseRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _expenseService.AddExpenseAsync(request, cancellationToken);
        return Ok(result);
    }
}
