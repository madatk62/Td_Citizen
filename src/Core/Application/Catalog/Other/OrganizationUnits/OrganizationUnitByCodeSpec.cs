namespace TD.CitizenAPI.Application.Catalog.OrganizationUnits;

public class OrganizationUnitByCodeSpec : Specification<OrganizationUnit>, ISingleResultSpecification
{
    public OrganizationUnitByCodeSpec(string code) =>
        Query.Where(b => b.Code == code);
}