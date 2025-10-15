using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserManagement.Models;

namespace UserManagement.Data.Entities;
public class UserActionLog
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public User User { get; set; } = default!;
    public UserAction Action { get; set; }
    public string ChangedValues { get; set; } = default!;
    public string ActionedBy { get; set; } = default!;
    public DateTime Timestamp { get; set; }
}

public enum UserAction
{
    Create,
    Update
}
