using System.Linq;

namespace TD.CitizenAPI.Application.Catalog.Hotlines;

public class HotlinesBySearchRequestSpec : EntitiesByPaginationFilterSpec<Hotline, HotlineDto>
{
    public HotlinesBySearchRequestSpec(SearchHotlinesRequest request)
        : base(request) =>
        Query
            .Include(p => p.HotlineCategory)
        .Include(p => p.Commune).Include(p => p.District).Include(p => p.Province)
            .Where(p => p.HotlineCategoryId.Equals(request.HotlineCategoryId!.Value), request.HotlineCategoryId.HasValue)
        .Where(p => p.CommuneId.Equals(request.CommuneId!.Value), request.CommuneId.HasValue)
        .Where(p => p.ProvinceId.Equals(request.ProvinceId!.Value), request.ProvinceId.HasValue)
        .Where(p => p.DistrictId.Equals(request.DistrictId!.Value), request.DistrictId.HasValue)
        ;

}