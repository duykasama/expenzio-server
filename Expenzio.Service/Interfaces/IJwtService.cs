using Expenzio.Domain.Entities;

namespace Expenzio.Service.Interfaces;

public interface IJwtService
{
    string GenerateAccessToken(ExpenzioUser user, IEnumerable<string> roles);
    string GenerateRefreshToken(Guid userId);
}
