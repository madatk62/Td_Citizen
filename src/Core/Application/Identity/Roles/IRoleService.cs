using TD.CitizenAPI.Application.Identity.Users;

namespace TD.CitizenAPI.Application.Identity.Roles;

public interface IRoleService : ITransientService
{
    Task<List<RoleDto>> GetListAsync(CancellationToken cancellationToken);

    Task<int> GetCountAsync(CancellationToken cancellationToken);

    Task<bool> ExistsAsync(string roleName, string? excludeId);

    Task<RoleDto> GetByIdAsync(string id);

    Task<RoleDto> GetByIdWithPermissionsAsync(string roleId, CancellationToken cancellationToken);
    Task<RoleUserDto> GetByIdWithUserAsync(string roleId, CancellationToken cancellationToken);

    Task<PaginationResponse<UserDto>> GetUsersByIdRoleAsync(string roleId, UserListFilter filter, CancellationToken cancellationToken);
    


    Task<string> CreateOrUpdateAsync(CreateOrUpdateRoleRequest request);

    Task<string> UpdatePermissionsAsync(UpdateRolePermissionsRequest request, CancellationToken cancellationToken);

    Task<string> DeleteAsync(string id);
}