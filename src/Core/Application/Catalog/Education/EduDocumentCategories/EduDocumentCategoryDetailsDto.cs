namespace TD.CitizenAPI.Application.Catalog.EduDocumentCategories;

public class EduDocumentCategoryDetailsDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Code { get; set; } = default!;
    public string? Icon { get; set; }
    public string? Image { get; set; }
    public string? CoverImage { get; set; }
    public string? Description { get; set; }
    public int? Order { get; set; }
    public Guid? EduDocumentCatalogueId { get; set; }

    public EduDocumentCatalogue? EduDocumentCatalogue { get; set; }
}