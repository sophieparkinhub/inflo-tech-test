using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.DTOs;
using UserManagement.Application.Mapper;
using UserManagement.Data.Entities;
using UserManagement.Infrastructure.Interfaces;
using UserManagement.Models;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserActionLogService _userActionLogService;
    private readonly IUserMapper _userMapper;

    public UserService(IUserRepository userRepository, IUserActionLogService userActionLogService, IUserMapper userMapper)
    {
        _userRepository = userRepository;
        _userActionLogService = userActionLogService;
        _userMapper = userMapper;
    }

    public async Task<IEnumerable<UserDto>> FilterByActive(bool isActive, CancellationToken cancellationToken)
    {
        var userEntities = await _userRepository.GetUsersByIsActiveFlagAsync(isActive, cancellationToken);
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

        var changes = JsonSerializer.Serialize(userEntity);
        await _userActionLogService.LogAsync(userEntity.Id, UserAction.Create, changes, cancellationToken);
    }

    public async Task Update(UserDto user, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetUserByIdNoTrackingAsync(user.Id, cancellationToken);
        if (existingUser == null) return;

        var userEntity = _userMapper.MapDtoToEntity(user);
        await _userRepository.UpdateAsync(userEntity, cancellationToken);

        var changes = GetUserValueChanges(existingUser, userEntity);
        await _userActionLogService.LogAsync(userEntity.Id, UserAction.Update, changedValues: string.Join(", ", changes), cancellationToken);
    }

    private static IEnumerable<string> GetUserValueChanges(User existingUser, User updatedUser)
    {
        if (existingUser.Forename != updatedUser.Forename)
           yield return $"Forename: '{existingUser.Forename}' > '{updatedUser.Forename}'";

        if (existingUser.Surname != updatedUser.Surname)
            yield return $"Surname: '{existingUser.Surname}' > '{updatedUser.Surname}'";

        if (existingUser.Email != updatedUser.Email)
            yield return $"Email: '{existingUser.Email}' > '{updatedUser.Email}'";

        if (existingUser.DateOfBirth != updatedUser.DateOfBirth)
            yield return $"Date of Birth: '{existingUser.DateOfBirth}' > '{updatedUser.DateOfBirth}'";

        if (existingUser.IsActive != updatedUser.IsActive)
            yield return $"Is Active: '{existingUser.IsActive}' > '{updatedUser.IsActive}'";
    }

    public async Task<bool> Delete(long id, CancellationToken cancellationToken)
    {
        var userEntity = await _userRepository.GetByIdAsync(id);
        if (userEntity == null) return false;

        await _userRepository.DeleteAsync(userEntity, cancellationToken);
        return true;
    }
}
