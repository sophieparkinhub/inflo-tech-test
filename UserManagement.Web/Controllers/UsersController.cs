using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.DTOs;
using UserManagement.Services.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

public class UsersController : Controller
{
    private readonly IUserService _userService;
    private readonly IUserActionLogService _userActionLogService;
    public UsersController(IUserService userService, IUserActionLogService userActionLogService)
    {
        _userService = userService;
        _userActionLogService = userActionLogService;
    }

    [HttpGet]
    public async Task<ViewResult> List(string filter, CancellationToken cancellationToken)
    {
        var users = filter switch
        {
            "active" => await _userService.FilterByActive(true, cancellationToken),
            "nonactive" => await _userService.FilterByActive(false, cancellationToken),
            _ => await _userService.GetAll(cancellationToken),
        };

        var items = users.Select(ConvertDtoToUserViewModel);

        var model = new UserListViewModel
        {
            Items = items.ToList()
        };

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Details(long id, CancellationToken cancellationToken)
    {

        var user = await _userService.GetById(id);

        if(user == null)
            return NotFound();

        var userViewModel = ConvertDtoToUserViewModel(user);

        var userLogs = await _userActionLogService.GetUserActionLogs(id, cancellationToken);
        userViewModel.Logs = userLogs?.ToList() ?? [];

        return View(userViewModel);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserListItemViewModel userViewModel, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid)
        {
            var user = new UserDto()
            {
                Forename = userViewModel.Forename!,
                Surname = userViewModel.Surname!,
                DateOfBirth = userViewModel.DateOfBirth!.Value,
                Email = userViewModel.Email!,
                IsActive = userViewModel.IsActive,
            };

            await _userService.Create(user, cancellationToken);

            TempData["UserSuccessMessage"] = $"Successfully created user for {userViewModel.Forename} {userViewModel.Surname}";
            return RedirectToAction("List");
        }

        return View(userViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(long id)
    {

        var user = await _userService.GetById(id);

        if (user == null)
            return NotFound();

        var userViewModel = ConvertDtoToUserViewModel(user);

        return View(userViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UserListItemViewModel userViewModel, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid)
        {
            var user = new UserDto()
            {
                Id = userViewModel.Id,
                Forename = userViewModel.Forename!,
                Surname = userViewModel.Surname!,
                DateOfBirth = userViewModel.DateOfBirth!.Value,
                Email = userViewModel.Email!,
                IsActive = userViewModel.IsActive,
            };

            await _userService.Update(user, cancellationToken);

            TempData["UserSuccessMessage"] = $"Successfully updated user {userViewModel.Forename} {userViewModel.Surname}";

            return RedirectToAction("List");
        }

        return View(userViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(long id, bool deleteErrored = false)
    {

        var user = await _userService.GetById(id);

        if (user == null)
            return NotFound();

        var userViewModel = ConvertDtoToUserViewModel(user);

        if (deleteErrored)
        {
            TempData["UserErrorMessage"] = $"Failed to delete user {userViewModel.Forename} {userViewModel.Surname}";
        }

        return View(userViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        var userDeleted = await _userService.Delete(id, cancellationToken);

        if(userDeleted)
        {
            TempData["UserSuccessMessage"] = $"Successfully deleted user id {id}";
            return RedirectToAction("List");
        }


        return RedirectToAction(nameof(Delete), new { id = id, deleteErrored = true });
    }

    private UserListItemViewModel ConvertDtoToUserViewModel(UserDto userDto) => new UserListItemViewModel()
    {
        Id = userDto.Id,
        Forename = userDto.Forename,
        Surname = userDto.Surname,
        Email = userDto.Email,
        DateOfBirth = userDto.DateOfBirth,
        IsActive = userDto.IsActive
    };
}
