namespace UserManagement.Blazor.Pages.Logs;

public class UserLogItemViewModel
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string? UserFullname { get; set; }
    public UserAction Action { get; set; }
    public string? ActionedBy { get; set; }
    public string? ChangedValues { get; set; }
    public DateTime Timestamp { get; set; }
}

public enum UserAction
{
    Create,
    Update
}
