using Finbuckle.MultiTenant;
using TD.CitizenAPI.Application.Common.Events;
using TD.CitizenAPI.Application.Common.Exceptions;
using TD.CitizenAPI.Application.Common.Interfaces;
using TD.CitizenAPI.Application.Identity.Roles;
using TD.CitizenAPI.Domain.Identity;
using TD.CitizenAPI.Infrastructure.Persistence.Context;
using TD.CitizenAPI.Shared.Authorization;
using TD.CitizenAPI.Shared.Multitenancy;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using TD.CitizenAPI.Application.Identity.Users;
using TD.CitizenAPI.Application.Common.Models;
using Ardalis.Specification.EntityFrameworkCore;

namespace TD.CitizenAPI.Infrastructure.Identity;

internal class RoleService : IRoleService
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _db;
    private readonly IStringLocalizer<RoleService> _localizer;
    private readonly ICurrentUser _currentUser;
    private readonly ITenantInfo _currentTenant;
    private readonly IEventPublisher _events;

    public RoleService(
        RoleManager<ApplicationRole> roleManager,
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext db,
        IStringLocalizer<RoleService> localizer,
        ICurrentUser currentUser,
        ITenantInfo currentTenant,
        IEventPublisher events)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _db = db;
        _localizer = localizer;
        _currentUser = currentUser;
        _currentTenant = currentTenant;
        _events = events;
    }

    public async Task<List<RoleDto>> GetListAsync(CancellationToken cancellationToken) =>
        (await _roleManager.Roles.ToListAsync(cancellationToken))
            .Adapt<List<RoleDto>>();

    public async Task<int> GetCountAsync(CancellationToken cancellationToken) =>
        await _roleManager.Roles.CountAsync(cancellationToken);

    public async Task<bool> ExistsAsync(string roleName, string? excludeId) =>
        await _roleManager.FindByNameAsync(roleName)
            is ApplicationRole existingRole
            && existingRole.Id != excludeId;

    public async Task<RoleDto> GetByIdAsync(string id) =>
        await _db.Roles.SingleOrDefaultAsync(x => x.Id == id) is { } role
            ? role.Adapt<RoleDto>()
            : throw new NotFoundException(_localizer["Role Not Found"]);

    public async Task<RoleDto> GetByIdWithPermissionsAsync(string roleId, CancellationToken cancellationToken)
    {
        var role = await GetByIdAsync(roleId);

        role.Permissions = await _db.RoleClaims
            .Where(c => c.RoleId == roleId && c.ClaimType == FSHClaims.Permission)
            .Select(c => c.ClaimValue)
            .ToListAsync(cancellationToken);

        return role;
    }




    public async Task<string> CreateOrUpdateAsync(CreateOrUpdateRoleRequest request)
    {
        if (string.IsNullOrEmpty(request.Id))
        {
            // Create a new role.
            var role = new ApplicationRole(request.Name, request.Description);
            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                throw new InternalServerException(_localizer["Register role failed"], result.GetErrors(_localizer));
            }

            await _events.PublishAsync(new ApplicationRoleCreatedEvent(role.Id, role.Name));

            return role.Id;
        }
        else
        {
            // Update an existing role.
            var role = await _roleManager.FindByIdAsync(request.Id);

            _ = role ?? throw new NotFoundException(_localizer["Role Not Found"]);

            if (FSHRoles.IsDefault(role.Name))
            {
                throw new ConflictException(string.Format(_localizer["Not allowed to modify {0} Role."], role.Name));
            }

            role.Name = request.Name;
            role.NormalizedName = request.Name.ToUpperInvariant();
            role.Description = request.Description;
            var result = await _roleManager.UpdateAsync(role);

            if (!result.Succeeded)
            {
                throw new InternalServerException(_localizer["Update role failed"], result.GetErrors(_localizer));
            }

            await _events.PublishAsync(new ApplicationRoleUpdatedEvent(role.Id, role.Name));

            return role.Id;
        }
    }

    public async Task<string> UpdatePermissionsAsync(UpdateRolePermissionsRequest request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.RoleId);
        _ = role ?? throw new NotFoundException(_localizer["Role Not Found"]);
        if (role.Name == FSHRoles.Admin)
        {
            throw new ConflictException(_localizer["Not allowed to modify Permissions for this Role."]);
        }

        if (request.Permissions == null)
        {
            return _localizer["Permissions Updated."];
        }

        if (_currentTenant.Id != MultitenancyConstants.Root.Id)
        {
            // Remove Root Permissions if the Role is not created for Root Tenant.
            request.Permissions.RemoveAll(u => u.StartsWith("Permissions.Root."));
        }


        var currentClaims = await _roleManager.GetClaimsAsync(role);

        // Remove permissions that were previously selected
        foreach (var claim in currentClaims.Where(c => !request.Permissions.Any(p => p == c.Value)))
        {
            var removeResult = await _roleManager.RemoveClaimAsync(role, claim);
            if (!removeResult.Succeeded)
            {
                throw new InternalServerException(_localizer["Update permissions failed."], removeResult.GetErrors(_localizer));
            }
        }

        // Add all permissions that were not previously selected
        foreach (string permission in request.Permissions.Where(c => !currentClaims.Any(p => p.Value == c)))
        {
            if (!string.IsNullOrEmpty(permission))
            {
                _db.RoleClaims.Add(new ApplicationRoleClaim
                {
                    RoleId = role.Id,
                    ClaimType = FSHClaims.Permission,
                    ClaimValue = permission,
                    CreatedBy = _currentUser.GetUserId().ToString()
                });
                await _db.SaveChangesAsync(cancellationToken);
            }
        }

        await _events.PublishAsync(new ApplicationRoleUpdatedEvent(role.Id, role.Name, true));

        return _localizer["Permissions Updated."];
    }

    public async Task<string> DeleteAsync(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);

        _ = role ?? throw new NotFoundException(_localizer["Role Not Found"]);

        if (FSHRoles.IsDefault(role.Name))
        {
            throw new ConflictException(string.Format(_localizer["Not allowed to delete {0} Role."], role.Name));
        }

        if ((await _userManager.GetUsersInRoleAsync(role.Name)).Count > 0)
        {
            throw new ConflictException(string.Format(_localizer["Not allowed to delete {0} Role as it is being used."], role.Name));
        }

        await _roleManager.DeleteAsync(role);

        await _events.PublishAsync(new ApplicationRoleDeletedEvent(role.Id, role.Name));

        return string.Format(_localizer["Role {0} Deleted."], role.Name);
    }

    public async Task<RoleUserDto> GetByIdWithUserAsync(string id) =>
        await _db.Roles.SingleOrDefaultAsync(x => x.Id == id) is { } role
            ? role.Adapt<RoleUserDto>()
            : throw new NotFoundException(_localizer["Role Not Found"]);
    public async Task<RoleUserDto> GetByIdWithUserAsync(string roleId, CancellationToken cancellationToken)
    {
        var role = await GetByIdWithUserAsync(roleId);
        //role.UserNames =
        role.Users = (await _db.UserRoles
            .Where(c => c.RoleId == roleId)
            .Join(_db.Users, x => x.UserId, y => y.Id, (x, y) => y)
            .ToListAsync(cancellationToken)).Adapt<List<UserDto>>();

        return role;
    }


    public async Task<PaginationResponse<UserDto>> GetUsersByIdRoleAsync(string roleId, UserListFilter filter, CancellationToken cancellationToken)
    {
        var spec = new UserListFilterSpec(filter);
        var specPag = new UserListPaginationFilterSpec(filter);

        var users = await _db.Users
           .Join(_db.UserRoles.Where(x => x.RoleId == roleId), x => x.Id, y => y.UserId, (x, y) => x)
           .WithSpecification(specPag)
           .ProjectToType<UserDto>()
           .ToListAsync(cancellationToken);
        int count = await _db.Users
             .Join(_db.UserRoles.Where(x => x.RoleId == roleId), x => x.Id, y => y.UserId, (x, y) => x)
            .WithSpecification(spec)
            .CountAsync(cancellationToken);

        return new PaginationResponse<UserDto>(users, count, filter.PageNumber, filter.PageSize);

    }

}