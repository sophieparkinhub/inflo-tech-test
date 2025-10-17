using Microsoft.AspNetCore.Mvc;
using UserManagement.Api.Models;
using UserManagement.Data.Entities;
using UserManagement.Services.Interfaces;

namespace UserManagement.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LogsController(IUserActionLogService userActionLogService) : ControllerBase
{
    [HttpGet]
    [Route("get-list")]
    public async Task<IEnumerable<UserLogModel>> GetList(CancellationToken cancellationToken)
    {
        var userLogs = await userActionLogService.GetAll(cancellationToken);

        if (!userLogs.Any()) return new List<UserLogModel>();

        return userLogs.Select(ConvertEntityToModel);
    }

    private static UserLogModel ConvertEntityToModel(UserActionLog userActionLog) => new UserLogModel()
    {
        Id = userActionLog.Id,
        UserId = userActionLog.User.Id,
        UserFullname = $"{userActionLog.User.Forename} {userActionLog.User.Surname}",
        Action = userActionLog.Action,
        ActionedBy = userActionLog.ActionedBy,
        ChangedValues = userActionLog.ChangedValues,
        Timestamp = userActionLog.Timestamp
    };
}
