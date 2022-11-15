namespace TD.CitizenAPI.Application.Catalog.HoSoDienTus;

public class GiayToHoSoDienTuDto : IDto
{
    public Guid Id { get; set; }
    public string? IDCongDan { get; set; }
    public string? HoSoDienTuID { get; set; }
    public string? MaHoSoDienTu { get; set; }
    public string? TenGiayTo { get; set; }
    public string? MaGiayTo { get; set; }
    public string? DinhKem { get; set; }
    public string? SoGiayTo { get; set; }
    public string? LoaiGiayToID { get; set; }
    public string? TenLoaiGiayTo { get; set; }
    public string? NhomGiayToID { get; set; }
    public string? TenNhomGiayTo { get; set; }
}