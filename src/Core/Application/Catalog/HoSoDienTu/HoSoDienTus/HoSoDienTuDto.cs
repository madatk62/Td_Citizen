namespace TD.CitizenAPI.Application.Catalog.HoSoDienTus;

public class HoSoDienTuDto : IDto
{
    public Guid Id { get; set; }
    public string? IDCongDan { get; set; }
    public string? HoSoDienTuID { get; set; }
    public string? TaiKhoanTao { get; set; }
    public string? TenHoSo { get; set; }
    public string? MaHoSo { get; set; }
    public string? TenThuTuc { get; set; }
    public string? MaThuTuc { get; set; }
    public string? TenNhomHoSo { get; set; }
    public string? MaNhomHoSo { get; set; }
    public string? TenLoaiHoSo { get; set; }
    public string? MaLoaiHoSo { get; set; }
}