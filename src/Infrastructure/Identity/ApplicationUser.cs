using Microsoft.AspNetCore.Identity;

namespace TD.CitizenAPI.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; }
    public bool IsVerified { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Gender { get; set; }
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

    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    //Loai nguoi dung : cong dan : 0, can bo: 1
    public int? Type { get; set; }
    //Kiem tra co phai la tai khoan dong bo hay khong
    public bool? IsSync { get; set; }


    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public DateTime? CreatedOn { get; set; }


    public string? ObjectId { get; set; }
    public string? TechnicalId { get; set; }
}