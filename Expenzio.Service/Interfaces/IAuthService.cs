using Expenzio.Common.Interfaces;
using Expenzio.Domain.Models.Responses;
using Expenzio.Domain.Models.Requests.Authentication;

namespace Expenzio.Service.Interfaces;

public interface IAuthService : IAutoRegisterable
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
    Task<ApiResponse<TokenResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Generate access token based on provided refresh token
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>ApiResponse</returns>
    Task<ApiResponse<TokenResponse>> RefreshTokenAsync(CancellationToken cancellationToken = default);
}
