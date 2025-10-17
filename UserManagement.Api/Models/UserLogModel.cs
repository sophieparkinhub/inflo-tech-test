using UserManagement.Data.Entities;

namespace UserManagement.Api.Models;

public class UserLogModel
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string? UserFullname { get; set; }
    public UserAction Action { get; set; }
    public string? ActionedBy { get; set; }
    public string? ChangedValues { get; set; }
    public DateTime Timestamp { get; set; }
}
