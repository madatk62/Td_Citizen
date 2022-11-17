namespace TD.CitizenAPI.Application.Catalog.HoSoDienTus;

public class HoSoDienTuDetailsDto : IDto
{
    public Guid Id { get; set; }
    public string? IDCongDan { get; set; }
    public string? TaiKhoanTao { get; set; }
    public string? TenHoSo { get; set; }
    public string? MaHoSo { get; set; }
    public string? TenThuTuc { get; set; }
    public string? MaThuTuc { get; set; }
    public string? TenLinhVuc { get; set; }
    public string? MaLinhVuc { get; set; }
}