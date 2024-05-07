using Expenzio.Domain.Common;

namespace Expenzio.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TEntity> CreateSet<TEntity, TKey>() where TEntity : BaseEntity<TKey> where TKey : notnull;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
