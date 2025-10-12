using UserManagement.Application.DTOs;
using UserManagement.Models;

namespace UserManagement.Application.Mapper;
public interface IUserMapper
{
    User MapDtoToEntity(UserDto user);
    IEnumerable<UserDto> MapEntitiesToDtoList(IEnumerable<User> users);
    UserDto MapEntityToDto(User user);
}