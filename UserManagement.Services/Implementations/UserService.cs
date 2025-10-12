using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.DTOs;
using UserManagement.Application.Mapper;
using UserManagement.Infrastructure.Interfaces;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserMapper _userMapper;

    public UserService(IUserRepository userRepository, IUserMapper userMapper)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
    }

    public async Task<IEnumerable<UserDto>> FilterByActive(bool isActive, CancellationToken cancellationToken)
    {
        var userEntities = await _userRepository.GetUsersByIsActiveFlag(isActive, cancellationToken);
        return _userMapper.MapEntitiesToDtoList(userEntities);
    }

    public async Task<IEnumerable<UserDto>> GetAll(CancellationToken cancellationToken)
    {
        var userEntities = await _userRepository.GetAllAsync(cancellationToken);
        return _userMapper.MapEntitiesToDtoList(userEntities);
    }

    public async Task<UserDto?> GetById(long id)
    {
        var userEntity = await _userRepository.GetByIdAsync(id);

        if (userEntity == null) return null;

        return _userMapper.MapEntityToDto(userEntity);
    }

    public async Task Create(UserDto user, CancellationToken cancellationToken)
    {
        var userEntity = _userMapper.MapDtoToEntity(user);
        await _userRepository.AddAsync(userEntity, cancellationToken);
    }

    public async Task Update(UserDto user, CancellationToken cancellationToken)
    {
        var userEntity = _userMapper.MapDtoToEntity(user);
        await _userRepository.UpdateAsync(userEntity, cancellationToken);
    }

    public async Task<bool> Delete(long id, CancellationToken cancellationToken)
    {
        var userEntity = await _userRepository.GetByIdAsync(id);
        if (userEntity == null) return false;

        await _userRepository.DeleteAsync(userEntity, cancellationToken);
        return true;
    }
}
