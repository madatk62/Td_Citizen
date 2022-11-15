namespace TD.CitizenAPI.Application.Catalog.LoginLogs;

public class LoginLogDetailsDto : IDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = default!;
    public DateTime? CreatedOn { get; set; }
    public string? Ip { get; set; }
}