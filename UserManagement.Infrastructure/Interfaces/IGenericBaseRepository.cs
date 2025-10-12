namespace UserManagement.Infrastructure.Interfaces;

public interface IGenericBaseRepository<T> where T : class
{
    Task<T> AddAsync(T entity, CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
    Task<T?> GetByIdAsync(object id);
    Task UpdateAsync(T entity, CancellationToken cancellationToken);
    Task DeleteAsync(T entity, CancellationToken cancellationToken);
}
