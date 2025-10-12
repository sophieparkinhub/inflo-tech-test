using UserManagement.Application.DTOs;
using UserManagement.Models;

namespace UserManagement.Application.Mapper;
public class UserMapper : IUserMapper
{
    public UserDto MapEntityToDto(User user) =>
          new UserDto()
          {
              Id = user.Id,
              Forename = user.Forename,
              Surname = user.Surname,
              Email = user.Email,
              DateOfBirth = user.DateOfBirth,
              IsActive = user.IsActive
          };

    public User MapDtoToEntity(UserDto userDto) =>
        new User()
        {
            Id = userDto.Id,
            Forename = userDto.Forename,
            Surname = userDto.Surname,
            Email = userDto.Email,
            DateOfBirth = userDto.DateOfBirth,
            IsActive = userDto.IsActive
        };

    public IEnumerable<UserDto> MapEntitiesToDtoList(IEnumerable<User> users) => users.Select(MapEntityToDto);

}
