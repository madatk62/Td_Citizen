namespace TD.CitizenAPI.Application.Identity.Users;

public class UserPermissionDto
{
    public string UserName { get; set; } = default!;
    public List<string>? Permissions { get; set; }
}