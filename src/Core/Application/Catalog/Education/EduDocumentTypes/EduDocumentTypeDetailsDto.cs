namespace TD.CitizenAPI.Application.Catalog.EduDocumentTypes;

public class EduDocumentTypeDetailsDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Code { get; set; } = default!;
    public string? Image { get; set; }
    public string? Description { get; set; }
}