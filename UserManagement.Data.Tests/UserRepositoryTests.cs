

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using UserManagement.Data;
using UserManagement.Infrastructure.Implementations;
using UserManagement.Models;
using Xunit;

namespace UserManagement.Infrastructure.Tests;
public class UserRepositoryTests
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task GetUsersByIsActiveFlagAsync_WhenFlagIsActiveIsPassedIn_ReturnsListOfUsers(bool isActive)
    {
        // Arrange

        var users = new List<User>()
        {
            new User() { Forename = "Sally", Surname = "Smith", Email = "sallysmith@test.com", DateOfBirth = DateTime.Parse("12/03/1997"), IsActive = isActive },
            new User() { Forename = "John", Surname = "Doe", Email = "johndoe@test.com", DateOfBirth = DateTime.Parse("15/05/1945"), IsActive = isActive }
        }.AsQueryable();

        var context = SetUpDataContext();
        context.AddRange(users);
        context.SaveChanges();

        var repo = new UserRepository(context);

        // Act
        var result = await repo.GetUsersByIsActiveFlagAsync(isActive, _cancellationToken);

        // Assert
        result.Count().Should().Be(users.ToList().Count);
    }

    [Fact]
    public async Task GetUserByIdNoTrackingAsync_ReturnsUser()
    {
        // Arrange

        var user = new User()
        {
            Forename = "Terry",
            Surname = "Smith",
            Email = "terrysmith@test.com",
            DateOfBirth = DateTime.Parse("12/03/1999"),
            IsActive = true };

        var context = SetUpDataContext();
        context.Add(user);
        context.SaveChanges();

        var repo = new UserRepository(context);

        // Act
        var result = await repo.GetUserByIdNoTrackingAsync(user.Id, _cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<User>();
        result.Id.Should().Be(user.Id);
    }

    [Fact]
    public async Task GetUserByIdAsync_ReturnsUser()
    {
        // Arrange

        var user = new User()
        {
            Forename = "Peter",
            Surname = "Nown",
            Email = "peternown@test.com",
            DateOfBirth = DateTime.Parse("12/07/2001"),
            IsActive = true
        };

        var context = SetUpDataContext();
        context.Add(user);
        context.SaveChanges();

        var repo = new UserRepository(context);

        // Act
        var result = await repo.GetByIdAsync(user.Id);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<User>();
        result.Id.Should().Be(user.Id);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsListOfUsers()
    {
        // Arrange

        var users = new List<User>()
        {
            new User() { Forename = "Sally", Surname = "Smith", Email = "sallysmith@test.com", DateOfBirth = DateTime.Parse("12/03/1997"), IsActive = true },
            new User() { Forename = "John", Surname = "Doe", Email = "johndoe@test.com", DateOfBirth = DateTime.Parse("15/05/1945"), IsActive = false }
        }.AsQueryable();

        var context = SetUpDataContext();
        context.AddRange(users);
        context.SaveChanges();

        var repo = new UserRepository(context);

        // Act
        var result = await repo.GetAllAsync(_cancellationToken);

        // Assert
        result.Count().Should().Be(users.ToList().Count);
    }

    [Fact]
    public async Task AddAsync_WhenCreatedEntity_VerifyUserAdded()
    {
        // Arrange

        var user = new User()
        {
            Forename = "Peter",
            Surname = "Nown",
            Email = "peternown@test.com",
            DateOfBirth = DateTime.Parse("12/07/2001"),
            IsActive = true
        };

        var context = SetUpDataContext();

        var repo = new UserRepository(context);

        // Act
        await repo.AddAsync(user, _cancellationToken);

        // Assert
        var userAdded = context.Users!.FirstOrDefault();
        userAdded.Should().NotBeNull();
        userAdded.Forename.Should().Be(user.Forename);
        userAdded.Surname.Should().Be(user.Surname);
        userAdded.Email.Should().Be(user.Email);
        userAdded.IsActive.Should().BeTrue();
        userAdded.DateOfBirth.Should().Be(user.DateOfBirth);
    }

    [Fact]
    public async Task UpdateAsync_WhenUpdatedEntity_VerifyUserUpdated()
    {
        // Arrange

        var user = new User()
        {
            Forename = "Wren",
            Surname = "Kellows",
            Email = "wrenkellows@test.com",
            DateOfBirth = DateTime.Parse("12/07/1990"),
            IsActive = true
        };

        var context = SetUpDataContext();
        context.Add(user);
        context.SaveChanges();

        user.IsActive = false;

        var repo = new UserRepository(context);

        // Act
        await repo.UpdateAsync(user, _cancellationToken);

        // Assert
        var userUpdated = context.Users!.FirstOrDefault();
        userUpdated.Should().NotBeNull();
        userUpdated.IsActive.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteAsync_WhenDeletedEntity_VerifyUserDeleted()
    {
        // Arrange

        var user = new User()
        {
            Forename = "Bye",
            Surname = "Now",
            Email = "byenow@test.com",
            DateOfBirth = DateTime.Parse("12/07/1999"),
            IsActive = true
        };

        var context = SetUpDataContext();
        context.Add(user);
        context.SaveChanges();

        var repo = new UserRepository(context);

        // Act
        await repo.DeleteAsync(user, _cancellationToken);

        // Assert
        context.Users.Should().BeEmpty();
    }



    private static DataContext SetUpDataContext()
    {
        var dbOptions = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new DataContext(dbOptions);
        return context;
    }

    private CancellationToken _cancellationToken = new CancellationToken();
}
