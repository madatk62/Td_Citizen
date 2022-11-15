using TD.CitizenAPI.Application.Common.Caching;
using TD.CitizenAPI.Application.Common.Exceptions;
using TD.CitizenAPI.Shared.Authorization;
using Microsoft.EntityFrameworkCore;
using TD.CitizenAPI.Application.Identity.Users;
using TD.CitizenAPI.Shared.Multitenancy;
using Microsoft.AspNetCore.Identity;

namespace TD.CitizenAPI.Infrastructure.Identity;

internal partial class UserService
{
    
    public async Task<List<string>> GetPermissionsAsyncByUserName(string userName, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(userName);

        _ = user ?? throw new NotFoundException(_localizer["User Not Found."]);

        var userRoles = await _userManager.GetRolesAsync(user);
        var permissions = new List<string>();
        foreach (var role in await _roleManager.Roles
            .Where(r => userRoles.Contains(r.Name))
            .ToListAsync(cancellationToken))
        {
            permissions.AddRange(await _db.RoleClaims
                .Where(rc => rc.RoleId == role.Id && rc.ClaimType == FSHClaims.Permission)
                .Select(rc => rc.ClaimValue)
                .ToListAsync(cancellationToken));
        }

        return permissions.Distinct().ToList();
    }

    public async Task<string> UpdatePermissionsAsync( UpdateUserPermissionsRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        _ = user ?? throw new NotFoundException(_localizer["User Not Found."]);

        if (request.Permissions == null)
        {
            return _localizer["Permissions Updated."];
        }

        if (_currentTenant.Id != MultitenancyConstants.Root.Id)
        {
            // Remove Root Permissions if the Role is not created for Root Tenant.
            request.Permissions.RemoveAll(u => u.StartsWith("Permissions.Root."));
        }

        var currentClaims = await _userManager.GetClaimsAsync(user);
        foreach (var claim in currentClaims.Where(c => !request.Permissions.Any(p => p == c.Value)))
        {
            var removeResult = await _userManager.RemoveClaimAsync(user, claim);
            if (!removeResult.Succeeded)
            {
                throw new InternalServerException(_localizer["Update permissions failed."], removeResult.GetErrors(_localizer));
            }
        }

        foreach (string permission in request.Permissions.Where(c => !currentClaims.Any(p => p.Value == c)))
        {
            if (!string.IsNullOrEmpty(permission))
            {
                _db.UserClaims.Add(new IdentityUserClaim<string>
                {
                    UserId = user.Id,
                    ClaimType = FSHClaims.Permission,
                    ClaimValue = permission,
                });
                await _db.SaveChangesAsync(cancellationToken);
            }
        }


        return _localizer["Permissions Updated."];
    }

    public async Task<UserPermissionDto> GetByUserNameWithPermissionsAsync(string userName, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(userName);
        _ = user ?? throw new NotFoundException(_localizer["User Not Found."]);

        var permissions = new List<string>();

        var tmp = new UserPermissionDto();
        tmp.UserName = user.UserName;

        permissions.AddRange(await _db.UserClaims
               .Where(rc => rc.UserId == user.Id && rc.ClaimType == FSHClaims.Permission)
               .Select(rc => rc.ClaimValue)
               .ToListAsync(cancellationToken));

        tmp.Permissions = permissions;
        return tmp;
    }

    public async Task<List<string>> GetPermissionsAsync(string userId, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(userId);

        _ = user ?? throw new NotFoundException(_localizer["User Not Found."]);

        var userRoles = await _userManager.GetRolesAsync(user);
        var permissions = new List<string>();
        foreach (var role in await _roleManager.Roles
            .Where(r => userRoles.Contains(r.Name))
            .ToListAsync(cancellationToken))
        {
            permissions.AddRange(await _db.RoleClaims
                .Where(rc => rc.RoleId == role.Id && rc.ClaimType == FSHClaims.Permission)
                .Select(rc => rc.ClaimValue)
                .ToListAsync(cancellationToken));
        }

        return permissions.Distinct().ToList();
    }

    public async Task<bool> HasPermissionAsync(string userId, string permission, CancellationToken cancellationToken)
    {
        var permissions = await _cache.GetOrSetAsync(
            _cacheKeys.GetCacheKey(FSHClaims.Permission, userId),
            () => GetPermissionsAsync(userId, cancellationToken),
            cancellationToken: cancellationToken);

        return permissions?.Contains(permission) ?? false;
    }

    public Task InvalidatePermissionCacheAsync(string userId, CancellationToken cancellationToken) =>
        _cache.RemoveAsync(_cacheKeys.GetCacheKey(FSHClaims.Permission, userId), cancellationToken);
}