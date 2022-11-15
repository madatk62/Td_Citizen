namespace TD.CitizenAPI.Application.Catalog.GovNews;

public class GovNewByIdWithIncludeSpec : Specification<GovNew, GovNewDetailsDto>, ISingleResultSpecification
{
    public GovNewByIdWithIncludeSpec(Guid id) =>
        Query
            .Where(p => p.Id == id)
            .Include(p => p.Commune)
            .Include(p => p.Province)
            .Include(p => p.District)
            .Include(p => p.GovNewCategory);
}