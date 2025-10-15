using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Data.Entities;
using UserManagement.Services.Interfaces;
using UserManagement.Web.Models.Logs;

namespace UserManagement.Web.Controllers;
public class LogsController : Controller
{
    private readonly IUserActionLogService _userActionLogService;

    public LogsController(IUserActionLogService userActionLogService)
    {
        _userActionLogService = userActionLogService;
    }

    [HttpGet]
    public async Task<IActionResult> List(string filter, CancellationToken cancellationToken)
    {
        var logs = filter switch
        {
            "Create" => await _userActionLogService.GetAllByAction(UserAction.Create, cancellationToken),
            "Update" => await _userActionLogService.GetAllByAction(UserAction.Update, cancellationToken),
            _ => await _userActionLogService.GetAll(cancellationToken),
        };

        var logListViewModel = new LogsListViewModel()
        {
            Items = logs?.Select(ConvertLogModelToViewModel).ToList() ?? new()
        };

        return View(logListViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Details(long id)
    {
        var log = await _userActionLogService.GetById(id);

        if(log == null)
            return NotFound();

        var logViewModel = ConvertLogModelToViewModel(log);
        return View(logViewModel);
    }

    private static LogsListItemViewModel ConvertLogModelToViewModel(UserActionLog userActionLog) =>
        new LogsListItemViewModel
        {
            Id = userActionLog.Id,
            UserId = userActionLog.User.Id,
            UserFullname = $"{userActionLog.User.Forename} {userActionLog.User.Surname}",
            Action = userActionLog.Action,
            ActionedBy = userActionLog.ActionedBy,
            ChangedValues = userActionLog.ChangedValues,
            Timestamp = userActionLog.Timestamp,
        };
}
