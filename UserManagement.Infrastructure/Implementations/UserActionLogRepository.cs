using Microsoft.EntityFrameworkCore;
using UserManagement.Data;
using UserManagement.Data.Entities;
using UserManagement.Infrastructure.Interfaces;

namespace UserManagement.Infrastructure.Implementations;

public class UserActionLogRepository : GenericBaseRepository<UserActionLog>, IUserActionLogRepository
{
    public UserActionLogRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public async Task<IEnumerable<UserActionLog>> GetAllByActionAsync(UserAction action, CancellationToken cancellationToken)
    => await _dbSet.Where(x => x.Action == action).ToListAsync(cancellationToken);

    public async Task<IEnumerable<UserActionLog>> GetAllUserActionLogsByUserIdAsync(long userId, CancellationToken cancellationToken)
        => await _dbSet.Where(x => x.User.Id == userId).ToListAsync(cancellationToken);
}

