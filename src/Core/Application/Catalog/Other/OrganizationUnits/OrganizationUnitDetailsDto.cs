using TD.CitizenAPI.Application.Catalog.Areas;

namespace TD.CitizenAPI.Application.Catalog.OrganizationUnits;

public class OrganizationUnitDetailsDto : IDto
{
    public Guid Id { get; set; }
    public Guid? ParentId { get; set; }
    public Guid? AreaId { get; set; }

    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Code { get; set; }

    public string? FullCode { get; set; }
    public string? ParentCode { get; set; }
    public int? Order { get; set; }
    public string? Type { get; set; }
    public virtual OrganizationUnitDto? Parent { get; set; }
    public virtual AreaDto? Area { get; set; }

}