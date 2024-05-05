using Expenzio.Domain.Models.Responses;
using Expenzio.Domain.Models.Requests.Authentication;
using Expenzio.Common.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Expenzio.Service.Interfaces;

/// <summary>
/// Service for authentication.
/// </summary>
/// <remarks>
/// This class contains methods for user authentication.
/// </remarks>
[AutoRegister(ServiceLifetime.Scoped)]
public interface IAuthService
{
    /// <summary>
    /// Register user
    /// </summary>
    /// <param name="request">Register request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>ApiResponse</returns>
    Task<ApiResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Login user
    /// </summary>
    /// <param name="request">Login request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>ApiResponse</returns>
    Task<ApiResponse<TokenResponse>> LogInAsync(LoginRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Generate access token based on provided refresh token
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>ApiResponse</returns>
    Task<ApiResponse<TokenResponse>> RefreshTokenAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Log out user
    /// </summary>
    /// <returns>ApiResponse</returns>
    Task<ApiResponse> LogOutAsync();
}
