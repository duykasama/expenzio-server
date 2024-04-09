using System.Linq.Expressions;
using Expenzio.DAL.Data;
using Expenzio.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Expenzio.DAL.Implementations;

public abstract class GenericRepository<T> : IGenericRepository<T> where T : class 
{
    private readonly ExpenzioDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(ExpenzioDbContext context) {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public void Add(T entity)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public void AddMany(IEnumerable<T> entities)
    {
        throw new NotImplementedException();
    }

    public Task AddManyAsync(IEnumerable<T> entities)
    {
        throw new NotImplementedException();
    }

    public long Count(Expression<Func<T, bool>> predicate)
    {
        return 999L;
    }

    public Task<long> CountAsync(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public void DeleteMany(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task DeleteManyAsync(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public bool Exists(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public T? Get(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<T>> GetAllAsync()
    {
        Console.WriteLine(_dbSet.AsQueryable());
        return Task.FromResult(_dbSet.AsQueryable().AsNoTracking());
    }

    public Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public void Update(T entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public void UpdateMany(IEnumerable<T> entities)
    {
        throw new NotImplementedException();
    }

    public void UpdateMany(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task UpdateManyAsync(IEnumerable<T> entities)
    {
        throw new NotImplementedException();
    }

    public Task UpdateManyAsync(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }
}
