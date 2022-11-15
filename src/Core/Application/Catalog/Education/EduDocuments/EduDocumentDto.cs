namespace TD.CitizenAPI.Application.Catalog.EduDocuments;

public class EduDocumentDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Image { get; set; }
    public string? Description { get; set; }
    public int? ViewQuantity { get; set; }
    public int? DownloadQuantity { get; set; }
   
    public Guid? EduDocumentCategoryId { get; set; }
    public Guid? EduDocumentTypeId { get; set; }
    public virtual EduDocumentCategory? EduDocumentCategory { get; set; }
    public virtual EduDocumentType? EduDocumentType { get; set; }
}