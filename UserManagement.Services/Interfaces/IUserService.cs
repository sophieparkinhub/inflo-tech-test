using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.DTOs;

namespace UserManagement.Services.Interfaces;
public interface IUserService
{
    Task Create(UserDto user, CancellationToken cancellationToken);
    Task<bool> Delete(long id, CancellationToken cancellationToken);
    Task<IEnumerable<UserDto>> FilterByActive(bool isActive, CancellationToken cancellationToken);
    Task<IEnumerable<UserDto>> GetAll(CancellationToken cancellationToken);
    Task<UserDto?> GetById(long id);
    Task Update(UserDto user, CancellationToken cancellationToken);
}
