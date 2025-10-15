using UserManagement.Models;

namespace UserManagement.Infrastructure.Interfaces;
public interface IUserRepository : IGenericBaseRepository<User>
{
    Task<User?> GetUserByIdNoTrackingAsync(long id, CancellationToken cancellationToken);
    Task<IEnumerable<User>> GetUsersByIsActiveFlagAsync(bool isActiveFlag, CancellationToken cancellationToken);
}
