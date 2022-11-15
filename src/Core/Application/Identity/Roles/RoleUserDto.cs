using TD.CitizenAPI.Application.Identity.Users;

namespace TD.CitizenAPI.Application.Identity.Roles;

public class RoleUserDto
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public List<UserDto>? Users { get; set; }
}