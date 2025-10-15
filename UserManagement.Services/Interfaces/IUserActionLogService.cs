using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Entities;

namespace UserManagement.Services.Interfaces;
public interface IUserActionLogService
{
    Task<IEnumerable<UserActionLog>> GetAll(CancellationToken cancellationToken);
    Task<IEnumerable<UserActionLog>> GetAllByAction(UserAction action, CancellationToken cancellationToken);
    Task<UserActionLog?> GetById(long id);
    Task<IEnumerable<UserActionLog>> GetUserActionLogs(long userId, CancellationToken cancellationToken);
    Task LogAsync(long userId, UserAction action, string changedValues, CancellationToken cancellationToken);
}
