using Microsoft.AspNetCore.Mvc;
using UserManagement.Api.Models;
using UserManagement.Application.DTOs;
using UserManagement.Services.Interfaces;

namespace UserManagement.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpGet]
    [Route("get-list")]
    public async Task<IEnumerable<UserModel>> GetList(CancellationToken cancellationToken)
    {
        var userLogs = await userService.GetAll(cancellationToken);

        if (!userLogs.Any()) return new List<UserModel>();

        return userLogs.Select(ConvertDtoToModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserModel model, CancellationToken cancellationToken)
    {
        var userDto = new UserDto()
        {
            Forename = model.Forename,
            Surname = model.Surname,
            DateOfBirth = model.DateOfBirth,
            Email = model.Email,
            IsActive = model.IsActive
        };

        await userService.Create(userDto, cancellationToken);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        var userDeleted = await userService.Delete(id, cancellationToken);
        return userDeleted ? StatusCode(StatusCodes.Status200OK) : StatusCode(StatusCodes.Status500InternalServerError);
    }

    private UserModel ConvertDtoToModel(UserDto userDto) =>
        new UserModel(userDto.Id, userDto.Forename, userDto.Surname, userDto.Email, userDto.DateOfBirth, userDto.IsActive);

}
