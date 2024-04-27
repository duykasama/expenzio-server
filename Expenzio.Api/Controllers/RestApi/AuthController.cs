using Asp.Versioning;
using Expenzio.Api.Controllers.RestApi.Base;
using Expenzio.Domain.Models.Requests.Authentication;
using Expenzio.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Expenzio.Api.Controllers.RestApi;

[ApiVersion(1)]
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
            async () => await _authService.RegisterAsync(request, cancellationToken)
        );
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken = default)
    {
        return await ExecuteAsync(
            async () => await _authService.LoginAsync(request, cancellationToken)
        );
    }
}
