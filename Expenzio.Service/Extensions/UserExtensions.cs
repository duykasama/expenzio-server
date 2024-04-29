using Expenzio.Domain.Entities;

namespace Expenzio.Service.Extensions;

public static class UserExtensions
{
    public static void SetCreationInfo(this ExpenzioUser user)
    {
        user.CreatedAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;
        user.IsDeleted = false;
    }

    public static void SetUpdateInfo(this ExpenzioUser user)
    {
        user.UpdatedAt = DateTime.UtcNow;
    }
}
