using Microsoft.EntityFrameworkCore;
using UserManagement.Data;
using UserManagement.Infrastructure.Interfaces;

namespace UserManagement.Infrastructure.Implementations;

public class GenericBaseRepository<T> : IGenericBaseRepository<T> where T : class
{
    protected readonly DataContext _dataContext;
    protected readonly DbSet<T> _dbSet;

    public GenericBaseRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
        _dbSet = _dataContext.Set<T>();
    }

    public async Task<T?> GetByIdAsync(object id) => await _dbSet.FindAsync(id);

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken) => await _dbSet.ToListAsync(cancellationToken);

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await _dataContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        _dataContext.Entry(entity).State = EntityState.Modified;
        await _dataContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        _dataContext.Entry(entity).State = EntityState.Deleted;
        await _dataContext.SaveChangesAsync(cancellationToken);
    }
}
