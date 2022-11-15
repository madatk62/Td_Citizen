using TD.CitizenAPI.Application.Catalog.Areas;

namespace TD.CitizenAPI.Application.Catalog.Hotlines;

public class HotlineDetailsDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Address { get; set; }
    public string? Code { get; set; }
    public string? Detail { get; set; }
    public string? OtherDetail { get; set; }
    public string? Phone { get; set; }
    public string? Image { get; set; }
    public bool? Active { get; set; }
    public int? Order { get; set; }
    public Guid? HotlineCategoryId { get; set; }

    public HotlineCategory? HotlineCategory { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    public Guid? ProvinceId { get; set; }
    public Guid? DistrictId { get; set; }
    public Guid? CommuneId { get; set; }

    public string? Description { get; set; }
    public DateTime? CreatedOn { get; set; }

    public virtual AreaDto? Province { get; set; }
    public virtual AreaDto? District { get; set; }
    public virtual AreaDto? Commune { get; set; }

}