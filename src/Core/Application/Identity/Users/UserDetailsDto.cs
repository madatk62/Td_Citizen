using TD.CitizenAPI.Application.Catalog.Areas;
using TD.CitizenAPI.Application.Catalog.Companies;

namespace TD.CitizenAPI.Application.Identity.Users;

public class UserDetailsDto
{
    public Guid Id { get; set; }

    public string? UserName { get; set; }

    public string? FullName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Gender { get; set; }
    public DateTime? DateOfBirth { get; set;}

    public bool IsActive { get; set; } = true;
    public bool IsVerified { get; set; }
    public bool EmailConfirmed { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public string? ImageUrl { get; set; }
    public int? Type { get; set; }

    public string? IdentityNumber { get; set; }
    public string? IdentityPlace { get; set; }
    public DateTime? IdentityDate { get; set; }
    //Nguyen Quan
    public string? PlaceOfOrigin { get; set; }
    //Thuong tru
    public string? PlaceOfDestination { get; set; }
    //Quoc tich
    public string? Nationality { get; set; }
    public string? ProvinceId { get; set; }
    public string? DistrictId { get; set; }
    public string? CommuneId { get; set; }
    public string? Address { get; set; }
    public Guid? CompanyId { get; set; }
    public CompanyDto? Company { get; set; }
    public AreaDto? Province { get; set; }
    public AreaDto? District { get; set; }
    public AreaDto? Commune { get; set; }
    public string? TechnicalId { get; set; }

}