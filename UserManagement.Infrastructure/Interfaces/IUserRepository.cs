using UserManagement.Models;

namespace UserManagement.Infrastructure.Interfaces;
public interface IUserRepository : IGenericBaseRepository<User>
{
    Task<IEnumerable<User>> GetUsersByIsActiveFlag(bool isActiveFlag, CancellationToken cancellationToken);
}
