using System.Linq;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Services.Interfaces;
using UserManagement.Web.Models.Users;
using UserManagement.WebMS.Controllers;

namespace UserManagement.Data.Tests;

public class UserControllerTests
{

    // TO DO

    private readonly Mock<IUserService> _userService = new();
    private UsersController CreateController() => new(_userService.Object);
}
