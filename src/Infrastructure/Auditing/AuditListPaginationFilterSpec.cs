using Ardalis.Specification;
using TD.CitizenAPI.Application.Auditing;
using TD.CitizenAPI.Application.Common.Specification;

namespace TD.CitizenAPI.Infrastructure.Auditing;

public class AuditListPaginationFilterSpec : EntitiesByPaginationFilterSpec<Trail>
{
    public AuditListPaginationFilterSpec(AuditListFilter request)
        : base(request) =>
        Query
            .Where(p => p.UserId == request.UserId, !string.IsNullOrEmpty(request.UserId))
            .Where(p => p.DateTime >= request.FromDate, request.FromDate.HasValue)
            .Where(p => p.DateTime <= request.ToDate, request.ToDate.HasValue)
        ;
}