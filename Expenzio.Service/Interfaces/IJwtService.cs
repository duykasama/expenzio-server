using Expenzio.Common.Interfaces;
using Expenzio.Domain.Entities;

namespace Expenzio.Service.Interfaces;

public interface IJwtService : IAutoRegisterable
{
    string GenerateAccessToken(ExpenzioUser user, IEnumerable<string> roles);
    string GenerateRefreshToken(Guid userId);
}
