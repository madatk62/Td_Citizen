namespace TD.CitizenAPI.Application.Catalog.PlaceTypes;

public class PlaceTypeByCodeSpec : Specification<PlaceType>, ISingleResultSpecification
{
    public PlaceTypeByCodeSpec(string code) =>
        Query.Where(b => b.Code == code);
}