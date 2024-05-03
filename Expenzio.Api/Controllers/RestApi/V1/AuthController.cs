using Expenzio.Api.Controllers.RestApi.Base;
using Expenzio.Domain.Models.Requests.Authentication;
using Expenzio.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Expenzio.Api.Controllers.RestApi.V1;

/// <summary>
/// Controller for authentication.
/// </summary>
/// <remarks>
/// This class contains endpoints for authentication.
/// </remarks>
[Route("api/v{version:apiVersion}/[controller]")]
public sealed class AuthController : BaseApiController
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
    [Route("log-in")]
    public async Task<IActionResult> LogIn([FromBody] LoginRequest request, CancellationToken cancellationToken = default)
    {
        return await ExecuteAsync(
            async () => await _authService.LogInAsync(request, cancellationToken).ConfigureAwait(false)
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

    [HttpPost]
    [Route("log-out")]
    public async Task<IActionResult> LogOut()
    {
        return await ExecuteAsync(
            async () => await _authService.LogOutAsync().ConfigureAwait(false)
        ).ConfigureAwait(false);
    }
}
