using Expenzio.Common.Attributes;
using Expenzio.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Expenzio.Service.Interfaces;

/// <summary>
/// Represents the JWT service.
/// </summary>
/// <remarks>
/// This interface contains methods related to JWT.
/// </remarks>
[AutoRegister(ServiceLifetime.Scoped)]
public interface IJwtService
{
    /// <summary>
    /// Generates an access token for the given user.
    /// </summary>
    /// <param name="user">The user to generate the token for.</param>
    /// <param name="roles">The roles of the user.</param>
    /// <returns>The generated access token.</returns>
    string GenerateAccessToken(ExpenzioUser user, IEnumerable<string> roles);

    /// <summary>
    /// Generates a refresh token for the given user.
    /// </summary>
    /// <param name="userId">The ID of the user to generate the token for.</param>
    /// <returns>The generated refresh token.</returns>
    string GenerateRefreshToken(Guid userId);
}
