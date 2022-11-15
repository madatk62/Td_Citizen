namespace TD.CitizenAPI.Application.Catalog.GovNews;

public class GovNewsBySearchRequestSpec : EntitiesByPaginationFilterSpec<GovNew, GovNewDto>
{
    public GovNewsBySearchRequestSpec(SearchGovNewsRequest request)
        : base(request) =>
        Query
        .Include(p => p.GovNewCategory)
        .Include(p => p.Commune).Include(p => p.District).Include(p => p.Province)
        .Where(p => p.GovNewCategoryId.Equals(request.GovNewCategoryId!.Value), request.GovNewCategoryId.HasValue)
        .Where(p => p.CommuneId.Equals(request.CommuneId!.Value), request.CommuneId.HasValue)
        .Where(p => p.ProvinceId.Equals(request.ProvinceId!.Value), request.ProvinceId.HasValue)
        .Where(p => p.DistrictId.Equals(request.DistrictId!.Value), request.DistrictId.HasValue)
        .Where(p => p.IsPublic == request.IsPublic, request.IsPublic.HasValue)
        .Where(p => p.IsPublic == request.IsStar, request.IsStar.HasValue)
        .Where(p => p.IsNotification == request.IsNotification, request.IsNotification.HasValue)
        ;

}