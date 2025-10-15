using UserManagement.Data.Entities;

namespace UserManagement.Infrastructure.Interfaces;
public interface IUserActionLogRepository : IGenericBaseRepository<UserActionLog>
{
    Task<IEnumerable<UserActionLog>> GetAllByActionAsync(UserAction action, CancellationToken cancellationToken);
    Task<IEnumerable<UserActionLog>> GetAllUserActionLogsByUserIdAsync(long userId, CancellationToken cancellationToken);
}
