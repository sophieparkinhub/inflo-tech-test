using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using UserManagement.Application.DTOs;
using UserManagement.Application.Mapper;
using UserManagement.Infrastructure.Interfaces;
using UserManagement.Models;
using UserManagement.Services.Domain.Implementations;

namespace UserManagement.Data.Tests;

public class UserServiceTests
{
    [Fact]
    public async Task FilterByActive_ReturnsListOfUserDtos_VerifyCalls()
    {
        // Arrange
        var service = CreateService();

        var isActive = true;

        var users = _fixture.CreateMany<User>();
        _userRepository.Setup(x => x.GetUsersByIsActiveFlag(isActive, _cancellationToken)).ReturnsAsync(users);

        var userDtos = _fixture.CreateMany<UserDto>(3);
        _userMapper.Setup(x => x.MapEntitiesToDtoList(users)).Returns(userDtos);

        // Act
        var result = await service.FilterByActive(isActive, _cancellationToken);

        // Assert
        result.Should().BeEquivalentTo(userDtos);
        _userRepository.Verify(x => x.GetUsersByIsActiveFlag(isActive, _cancellationToken), Times.Once);
        _userMapper.Verify(x => x.MapEntitiesToDtoList(users), Times.Once); 
    }

    [Fact]
    public async Task GetAll_ReturnsListOfUserDtos_VerifyCalls()
    {
        // Arrange
        var service = CreateService();

        var users = _fixture.CreateMany<User>();
        _userRepository.Setup(x => x.GetAllAsync(_cancellationToken)).ReturnsAsync(users);

        var userDtos = _fixture.CreateMany<UserDto>(3);
        _userMapper.Setup(x => x.MapEntitiesToDtoList(users)).Returns(userDtos);

        // Act
        var result = await service.GetAll(_cancellationToken);

        // Assert
        result.Should().BeEquivalentTo(userDtos);
        _userRepository.Verify(x => x.GetAllAsync(_cancellationToken), Times.Once);
        _userMapper.Verify(x => x.MapEntitiesToDtoList(users), Times.Once);
    }

    [Fact]
    public async Task GetById_WhenUserNotFound_ReturnsNull()
    {
        // Arrange
        var service = CreateService();
        var id = 1;
        _userRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(null as User);

        // Act
        var result = await service.GetById(id);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetById_WhenUserFound_ReturnsUserDto_VerifyCalls()
    {
        // Arrange
        var service = CreateService();
        long id = 1;

        var user = _fixture.Create<User>();
        _userRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(user);

        var userDto = _fixture.Create<UserDto>();
        _userMapper.Setup(x => x.MapEntityToDto(user)).Returns(userDto);

        // Act
        var result = await service.GetById(id);

        // Assert
        result.Should().Be(userDto);
        _userRepository.Verify(x => x.GetByIdAsync(id), Times.Once);
        _userMapper.Verify(x => x.MapEntityToDto(user), Times.Once);
    }

    [Fact]
    public async Task Create_VerifyCalls()
    {
        // Arrange
        var service = CreateService();

        var userDto = _fixture.Create<UserDto>();

        var user = _fixture.Create<User>();
        _userMapper.Setup(x => x.MapDtoToEntity(userDto)).Returns(user);

        _userRepository.Setup(x => x.AddAsync(user, _cancellationToken)).ReturnsAsync(user);

        // Act
        await service.Create(userDto, _cancellationToken);

        // Assert
        _userMapper.Verify(x => x.MapDtoToEntity(userDto), Times.Once);
        _userRepository.Verify(x => x.AddAsync(user, _cancellationToken), Times.Once);
    }

    [Fact]
    public async Task Update_VerifyCalls()
    {
        // Arrange
        var service = CreateService();

        var userDto = _fixture.Create<UserDto>();

        var user = _fixture.Create<User>();
        _userMapper.Setup(x => x.MapDtoToEntity(userDto)).Returns(user);

        // Act
        await service.Update(userDto, _cancellationToken);

        // Assert
        _userMapper.Verify(x => x.MapDtoToEntity(userDto), Times.Once);
        _userRepository.Verify(x => x.UpdateAsync(user, _cancellationToken), Times.Once);
    }

    [Fact]
    public async Task Delete_WhenUserNotFound_ReturnsFalse()
    {
        // Arrange
        var service = CreateService();
        long id = 1;
        _userRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(null as User);

        // Act
        var result = await service.Delete(id, _cancellationToken);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task Delete_WhenUserFound_ReturnsTrue_VerifyCalls()
    {
        // Arrange
        var service = CreateService();
        long id = 1;

        var user = _fixture.Create<User>();
        _userRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(user);

        _userRepository.Setup(x => x.DeleteAsync(user, _cancellationToken)).Returns(Task.CompletedTask);

        // Act
        var result = await service.Delete(id, _cancellationToken);

        // Assert
        result.Should().BeTrue();
        _userRepository.Verify(x => x.GetByIdAsync(id), Times.Once);
        _userRepository.Verify(x => x.DeleteAsync(user, _cancellationToken), Times.Once);
    }

    private CancellationToken _cancellationToken = new CancellationToken();
    private Fixture _fixture = new Fixture();
    private readonly Mock<IUserMapper> _userMapper = new();
    private readonly Mock<IUserRepository> _userRepository = new();
    private UserService CreateService() => new(_userRepository.Object, _userMapper.Object);
}
