using TD.CitizenAPI.Application.Catalog.Areas;

namespace TD.CitizenAPI.Application.Catalog.Companies;

public class CompanyExportDto : IDto
{
    public string Name { get; set; } = default!;
    public string? InternationalName { get; set; }
    public string? ShortName { get; set; }
    public string? TaxCode { get; set; }
    //Dia chi cong ty
    public string? Address { get; set; }

    //Dai dien
    public string? Representative { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Website { get; set; }
    public string? Email { get; set; }
    public string? ProfileVideo { get; set; }
    public string? Fax { get; set; }
    //Ngay cap
    public DateTime? DateOfIssue { get; set; }
    //Linh vuc kinh doanh
    public string? BusinessSector { get; set; }
    public string? Description { get; set; }
    //Quy mo cong ty
    public string? CompanySize { get; set; }
    public int? Status { get; set; }



}