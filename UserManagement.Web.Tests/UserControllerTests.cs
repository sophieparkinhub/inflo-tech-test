using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using UserManagement.Application.DTOs;
using UserManagement.Services.Interfaces;
using UserManagement.Web.Models.Users;
using UserManagement.WebMS.Controllers;

namespace UserManagement.Data.Tests;

public class UserControllerTests
{

    [Fact]
    public async Task List_WhenNoFilterServiceReturnsUsers_ModelMustContainUsers()
    {
        // Arrange
        var controller = CreateController();
        var users = _fixture.CreateMany<UserDto>(3);

        _userService
            .Setup(s => s.GetAll(_cancellationToken))
            .ReturnsAsync(users);

        // Act
        var result = await controller.List("", _cancellationToken);

        // Assert
        result.Model
            .Should().BeOfType<UserListViewModel>()
            .Which.Items.Should().BeEquivalentTo(users);
    }

    [Theory]
    [InlineData("active", true)]
    [InlineData("nonactive", false)]
    public async Task List_WhenUsingFilterServiceReturnsUsers_ModelMustContainUsers(string filter, bool IsActive)
    {
        // Arrange
        var controller = CreateController();
        var users = _fixture.CreateMany<UserDto>(3);

        _userService
            .Setup(s => s.FilterByActive(IsActive, _cancellationToken))
            .ReturnsAsync(users);

        // Act
        var result = await controller.List(filter, _cancellationToken);

        // Assert
        result.Model
            .Should().BeOfType<UserListViewModel>()
            .Which.Items.Should().BeEquivalentTo(users);
    }

    private CancellationToken _cancellationToken = new CancellationToken();
    private readonly Fixture _fixture = new Fixture();
    private readonly Mock<IUserService> _userService = new();
    private readonly Mock<IUserActionLogService> _userActionLogService = new();

    private UsersController CreateController() => new(_userService.Object, _userActionLogService.Object);
}
