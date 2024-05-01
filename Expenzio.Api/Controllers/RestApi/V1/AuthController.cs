using Expenzio.Api.Controllers.RestApi.Base;
using Expenzio.Domain.Models.Requests.Authentication;
using Expenzio.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Expenzio.Api.Controllers.RestApi.V1;

[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : BaseApiController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken = default)
    {
        return await ExecuteAsync(
            async () => await _authService.RegisterAsync(request, cancellationToken).ConfigureAwait(false)
        ).ConfigureAwait(false);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken = default)
    {
        return await ExecuteAsync(
            async () => await _authService.LoginAsync(request, cancellationToken).ConfigureAwait(false)
        ).ConfigureAwait(false);
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RereshToken(CancellationToken cancellationToken)
    {
        return await ExecuteAsync(
            async () => await _authService.RefreshTokenAsync(cancellationToken).ConfigureAwait(false)
        ).ConfigureAwait(false);
    }
}
