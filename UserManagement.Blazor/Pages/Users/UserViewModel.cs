﻿namespace UserManagement.Blazor.Pages.Users;

public class UserViewModel
{
    public long Id { get; set; }
    public string? Forename { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public bool IsActive { get; set; }
}
