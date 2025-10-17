namespace UserManagement.Api.Models;

public record UserModel(long Id, string Forename, string Surname, string Email, DateTime DateOfBirth, bool IsActive);
