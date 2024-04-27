using Microsoft.AspNetCore.Mvc;

namespace Expenzio.Api.Controllers.RestApi;

[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    protected async Task<IActionResult> ExecuteAsync<T>(Func<Task<T>> func)
    {
        try
        {
            var result = await func();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
