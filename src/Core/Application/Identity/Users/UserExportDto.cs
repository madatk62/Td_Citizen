using TD.CitizenAPI.Application.Catalog.Areas;

namespace TD.CitizenAPI.Application.Identity.Users;

public class UserExportDto
{

    public string? UserName { get; set; }

    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Gender { get; set; }

   

}