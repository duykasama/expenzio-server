using Expenzio.Domain.Models.Requests.Authentication;
using Expenzio.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Expenzio.Api.Controllers.RestApi;

internal class AuthController : BaseApiController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken = default)
    {
        return await ExecuteAsync(
            async () => await _authService.RegisterAsync(request, cancellationToken)
        );
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken = default)
    {
        return await ExecuteAsync(
            async () => await _authService.LoginAsync(request, cancellationToken)
        );
    }
}
