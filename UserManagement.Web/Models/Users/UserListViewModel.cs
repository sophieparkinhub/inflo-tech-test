using System;
using System.ComponentModel.DataAnnotations;
using UserManagement.Data.Entities;

namespace UserManagement.Web.Models.Users;

public class UserListViewModel
{
    public List<UserListItemViewModel> Items { get; set; } = new();
}

public class UserListItemViewModel
{
    public long Id { get; set; }
    [Required(ErrorMessage = "Forename is required")]
    public string? Forename { get; set; }
    [Required(ErrorMessage = "Surname is required")]
    public string? Surname { get; set; }
    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Date of Birth is required")]
    [Display(Name = "Date of Birth")]
    [DataType(DataType.Date)]
    public DateTime? DateOfBirth { get; set; }
    [Display(Name = "Is Active")]
    public bool IsActive { get; set; }

    public List<UserActionLog> Logs = new ();
}
