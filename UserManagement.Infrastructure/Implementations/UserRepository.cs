using Microsoft.EntityFrameworkCore;
using UserManagement.Data;
using UserManagement.Infrastructure.Interfaces;
using UserManagement.Models;

namespace UserManagement.Infrastructure.Implementations;
public class UserRepository : GenericBaseRepository<User>, IUserRepository
{
    public UserRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public async Task<IEnumerable<User>> GetUsersByIsActiveFlag(bool isActiveFlag, CancellationToken cancellationToken)
    => await _dbSet.Where(x => x.IsActive == isActiveFlag).ToListAsync(cancellationToken);
}
