using TD.CitizenAPI.Application.Identity.Roles;
using TD.CitizenAPI.Application.Identity.Users;

namespace TD.CitizenAPI.Host.Controllers.Identity;

public class RolesController : VersionNeutralApiController
{
    private readonly IRoleService _roleService;
    private readonly IUserService _userService;

    public RolesController(IRoleService roleService, IUserService userService) => (_roleService,_userService) = (roleService, userService);

    [HttpGet]
    [MustHavePermission(FSHAction.View, FSHResource.Roles)]
    [OpenApiOperation("Get a list of all roles.", "")]
    public Task<List<RoleDto>> GetListAsync(CancellationToken cancellationToken)
    {
        return _roleService.GetListAsync(cancellationToken);
    }

    [HttpGet("{id}")]
    [MustHavePermission(FSHAction.View, FSHResource.Roles)]
    [OpenApiOperation("Get role details.", "")]
    public Task<RoleDto> GetByIdAsync(string id)
    {
        return _roleService.GetByIdAsync(id);
    }

    [HttpGet("{id}/permissions")]
    [MustHavePermission(FSHAction.View, FSHResource.RoleClaims)]
    [OpenApiOperation("Get role details with its permissions.", "")]
    public Task<RoleDto> GetByIdWithPermissionsAsync(string id, CancellationToken cancellationToken)
    {
        return _roleService.GetByIdWithPermissionsAsync(id, cancellationToken);
    }
    [HttpGet("{id}/users")]
    [MustHavePermission(FSHAction.View, FSHResource.RoleClaims)]
    [OpenApiOperation("Get role details with its permissions.", "")]
    public Task<RoleUserDto> GetByIdWithUserAsync(string id, CancellationToken cancellationToken)
    {
        return _roleService.GetByIdWithUserAsync(id, cancellationToken);
    }

    [HttpPost("{id}/listusers")]
    [OpenApiOperation("Danh sách người dùng.", "")]
    public Task<PaginationResponse<UserDto>> SearchAsync(string id, UserListFilter request, CancellationToken cancellationToken)
    {
        return _roleService.GetUsersByIdRoleAsync(id, request, cancellationToken);
    }


    [HttpPost("{id}/users")]
    [OpenApiOperation("Them nguoi dung người dùng.", "")]
    public Task<PaginationResponse<UserDto>> AddUserAsync(string id, UserListFilter request, CancellationToken cancellationToken)
    {
        return _roleService.GetUsersByIdRoleAsync(id, request, cancellationToken);
    }


    [HttpPut("{id}/permissions")]
    [MustHavePermission(FSHAction.Update, FSHResource.RoleClaims)]
    [OpenApiOperation("Update a role's permissions.", "")]
    public async Task<ActionResult<string>> UpdatePermissionsAsync(string id, UpdateRolePermissionsRequest request, CancellationToken cancellationToken)
    {
        if (id != request.RoleId)
        {
            return BadRequest();
        }

        return Ok(await _roleService.UpdatePermissionsAsync(request, cancellationToken));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Roles)]
    [OpenApiOperation("Create or update a role.", "")]
    public Task<string> RegisterRoleAsync(CreateOrUpdateRoleRequest request)
    {
        return _roleService.CreateOrUpdateAsync(request);
    }

    [HttpDelete("{id}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Roles)]
    [OpenApiOperation("Delete a role.", "")]
    public Task<string> DeleteAsync(string id)
    {
        return _roleService.DeleteAsync(id);
    }
}