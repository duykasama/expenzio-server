using Expenzio.Domain.Common;

namespace Expenzio.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<T> CreateSet<T>() where T : BaseEntity;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
