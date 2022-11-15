namespace TD.CitizenAPI.Application.Catalog.GovNews;

public class GovNewDto : IDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Actor { get; set; }
    public string? Description { get; set; }

    public DateTime? Date { get; set; }
    public DateTime? CreatedOn { get; set; }
    public string? Image { get; set; }
    public string? Files { get; set; }
    public string? Source { get; set; }
    public int? ViewQuantity { get; set; }
    public int? Level { get; set; }

    public bool? IsStar { get; set; }
    public bool? IsPublic { get; set; }
    public bool? IsNotification { get; set; }


    public Guid? GovNewCategoryId { get; set; }
    public Guid? ProvinceId { get; set; }
    public Guid? DistrictId { get; set; }
    public Guid? CommuneId { get; set; }
    public virtual GovNewCategory? GovNewCategory { get; set; }

    public virtual Area? Province { get; set; }
    public virtual Area? District { get; set; }
    public virtual Area? Commune { get; set; }
}