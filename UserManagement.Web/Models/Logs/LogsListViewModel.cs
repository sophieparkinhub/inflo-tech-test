using System;
using System.ComponentModel.DataAnnotations;
using UserManagement.Data.Entities;

namespace UserManagement.Web.Models.Logs;

public class LogsListViewModel
{
    public List<LogsListItemViewModel> Items { get; set; } = new();
}

public class LogsListItemViewModel
{
    public long Id { get; set; }
    public long UserId { get; set; }
    [Display(Name = "User")]
    public string? UserFullname { get; set; }
    public UserAction Action { get; set; }
    [Display(Name = "Actioned By")]
    public string? ActionedBy { get; set; }
    [Display(Name = "Changed Values")]
    public string? ChangedValues { get; set; }
    public DateTime Timestamp { get; set; }
}

