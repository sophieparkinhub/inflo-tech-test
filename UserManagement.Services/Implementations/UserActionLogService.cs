using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Entities;
using UserManagement.Infrastructure.Interfaces;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services.Implementations;
public class UserActionLogService : IUserActionLogService
{
    private readonly IUserActionLogRepository _userActionLogRepository;
    private readonly IUserRepository _userRepository;

    public UserActionLogService(IUserActionLogRepository userActionLogRepository, IUserRepository userRepository)
    {
        _userActionLogRepository = userActionLogRepository;
        _userRepository = userRepository;
    }

    public async Task<UserActionLog?> GetById(long id) => await _userActionLogRepository.GetByIdAsync(id);

    public async Task<IEnumerable<UserActionLog>> GetAll(CancellationToken cancellationToken) => await _userActionLogRepository.GetAllAsync(cancellationToken);

    public async Task<IEnumerable<UserActionLog>> GetAllByAction(UserAction action, CancellationToken cancellationToken) => await _userActionLogRepository.GetAllByActionAsync(action, cancellationToken);

    public async Task<IEnumerable<UserActionLog>> GetUserActionLogs(long userId, CancellationToken cancellationToken)
        => await _userActionLogRepository.GetAllUserActionLogsByUserIdAsync(userId, cancellationToken);

    public async Task LogAsync(long userId, UserAction action, string changedValues, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
            return;

        var userActionLog = new UserActionLog()
        {
            User = user,
            Action = action,
            ChangedValues = changedValues,
            ActionedBy = "System, Authentication to do!",
            Timestamp = DateTime.UtcNow
        };

        await _userActionLogRepository.AddAsync(userActionLog, cancellationToken);
    }
}
