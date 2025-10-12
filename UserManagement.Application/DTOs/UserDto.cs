namespace UserManagement.Application.DTOs;

public class UserDto
{
    public long Id { get; set; }
    public required string Forename { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsActive { get; set; }
}
