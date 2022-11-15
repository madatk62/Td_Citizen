namespace TD.CitizenAPI.Application.Catalog.LoginLogs;

public class LoginLogDto : IDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = default!;
    public string? Ip { get; set; }
    public DateTime? CreatedOn { get; set; }
}