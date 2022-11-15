namespace TD.CitizenAPI.Application.Catalog.HoSoDienTus;

public class GiayToHoSoDienTuDetailsDto : IDto
{
    public Guid Id { get; set; }
    public string? HoSoDienTuID { get; set; }
    public string? MaHoSoDienTu { get; set; }
    public string? TenGiayTo { get; set; }
    public string? MaGiayTo { get; set; }
    public string? DinhKem { get; set; }
}