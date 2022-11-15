using TD.CitizenAPI.Application.Catalog.Brands;

namespace TD.CitizenAPI.Application.Catalog.HuongDanSuDungs;

public class HuongDanSuDungDetailsDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? File { get; set; }
    public string? Description { get; set; }
}