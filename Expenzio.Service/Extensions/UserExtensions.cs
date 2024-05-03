using Expenzio.Domain.Entities;

namespace Expenzio.Service.Extensions;

public static class UserExtensions
{
    /// <summary>
    /// Sets the creation info for the user.
    /// </summary>
    /// <param name="user">The user to set the creation info for.</param>
    /// <returns>The user with the creation info set.</returns>
    public static void SetCreationInfo(this ExpenzioUser user)
    {
        user.CreatedAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;
        user.IsDeleted = false;
    }

    /// <summary>
    /// Sets the update info for the user.
    /// </summary>
    /// <param name="user">The user to set the update info for.</param>
    /// <returns>The user with the update info set.</returns>
    public static void SetUpdateInfo(this ExpenzioUser user)
    {
        user.UpdatedAt = DateTime.UtcNow;
    }
}
