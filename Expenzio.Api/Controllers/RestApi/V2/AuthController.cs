using Asp.Versioning;
using Expenzio.Api.Controllers.RestApi.Base;
using Expenzio.Api.Resources;
using Expenzio.Domain.Models.Requests.Authentication;
using Expenzio.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Expenzio.Api.Controllers.RestApi.V2;

[Route("api/v{version:apiVersion}/[controller]")]
public sealed class AuthController : BaseApiController
{
    private readonly IAuthService _authService;
    private readonly IStringLocalizer<Messages> _localizer;

    public AuthController(IAuthService authService, IStringLocalizer<Messages> localizer)
    {
        _authService = authService;
        _localizer = localizer;
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
            async () => await _authService.LogInAsync(request, cancellationToken).ConfigureAwait(false)
        ).ConfigureAwait(false);
    }

    [HttpGet]
    [Route("test/{text}")]
    public async Task<IActionResult> Test(string text)
    {
        return await Task.FromResult(Ok(_localizer[text].Value));
    }
}
