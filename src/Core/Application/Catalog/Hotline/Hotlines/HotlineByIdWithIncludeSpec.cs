namespace TD.CitizenAPI.Application.Catalog.Hotlines;

public class HotlineByIdWithIncludeSpec : Specification<Hotline, HotlineDetailsDto>, ISingleResultSpecification
{
    public HotlineByIdWithIncludeSpec(Guid id) =>
        Query
            .Where(p => p.Id == id)
            .Include(p => p.Commune)
            .Include(p => p.Province)
            .Include(p => p.District)
            .Include(p => p.HotlineCategory);
}