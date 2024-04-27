using Asp.Versioning;
using Expenzio.Api.Controllers.RestApi.Base;
using Expenzio.Domain.Models.Requests.Authentication;
using Expenzio.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Expenzio.Api.Controllers.RestApi.V2;

[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : BaseApiController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost, MapToApiVersion(2)]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken = default)
    {
        return await ExecuteAsync(
            async () => await _authService.RegisterAsync(request, cancellationToken).ConfigureAwait(false)
        ).ConfigureAwait(false);
    }

    [HttpPost, MapToApiVersion(2)]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken = default)
    {
        return await ExecuteAsync(
            async () => await _authService.LoginAsync(request, cancellationToken).ConfigureAwait(false)
        ).ConfigureAwait(false);
    }
}
