using Expenzio.Common.Exceptions;
using Expenzio.Domain.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Expenzio.Api.Controllers.RestApi.Base;

[ApiController]
public class BaseApiController : ControllerBase
{
    protected async Task<IActionResult> ExecuteAsync<T>(Func<Task<T>> func)
    {
        try
        {
            var result = await func();
            if (result is ApiResponse apiResponse)
                return StatusCode(apiResponse.StatusCode, apiResponse);
            return Ok(result);
        }
        catch (Exception e)
        {
            if (e is ApiException apiException)
                return StatusCode(apiException.StatusCode, apiException.Message);

            return Problem(e.Message);
        }
    }
}
