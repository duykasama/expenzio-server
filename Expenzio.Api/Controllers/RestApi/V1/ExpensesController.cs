using Expenzio.Api.Controllers.RestApi.Base;
using Expenzio.Domain.Models.Requests;
using Expenzio.Domain.Models.Requests.Expense;
using Expenzio.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Expenzio.Api.Controllers.RestApi.V1;

/// <summary>
/// Controller for expenses.
/// </summary>
/// <remarks>
/// This class contains endpoints for expenses.
/// </remarks>
[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
public sealed class ExpensesController : BaseApiController 
{
    private readonly IExpenseService _expenseService;
    private readonly IExpenseCategoryService _expenseCategoryService;

    public ExpensesController(IExpenseService expenseService, IExpenseCategoryService expenseCategoryService)
    {
        _expenseService = expenseService;
        _expenseCategoryService = expenseCategoryService;
    }

    [HttpPost]
    public async Task<IActionResult> AddExpense([FromBody] CreateExpenseRequest request, CancellationToken cancellationToken = default)
    {
        return await ExecuteAsync(
            async () => await _expenseService.AddExpenseAsync(request, cancellationToken)
        );
    }

    [HttpPost]
    [Route("categories")]
    public async Task<IActionResult> CreateExpenseCategory([FromBody] CreateExpenseCategoryRequest request, CancellationToken cancellationToken = default)
    {
        return await ExecuteAsync(
            async () => await _expenseCategoryService.CreateExpenseCategoryAsync(request, cancellationToken)
        );
    }
}
