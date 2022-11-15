using Ardalis.Specification;
using TD.CitizenAPI.Application.Common.Specification;
using TD.CitizenAPI.Application.Identity.Users;

namespace TD.CitizenAPI.Infrastructure.Identity;

public class UserListPaginationFilterSpec : EntitiesByPaginationFilterSpec<ApplicationUser>
{
    public UserListPaginationFilterSpec(UserListFilter request)
        : base(request) =>
        Query
            .Where(p => p.Gender == request.Gender, !string.IsNullOrEmpty(request.Gender))
            .Where(p => p.IsActive == request.IsActive, request.IsActive.HasValue)
            .Where(p => p.IsSync == request.IsSync, request.IsSync.HasValue)
            .Where(p => p.Type == request.Type, request.Type.HasValue)
            .Where(p => p.IsVerified == request.IsVerified, request.IsVerified.HasValue)
            .Where(p => p.CreatedOn >= request.FromDate, request.FromDate.HasValue)
            .Where(p => p.CreatedOn <= request.ToDate, request.ToDate.HasValue)
        ;
}