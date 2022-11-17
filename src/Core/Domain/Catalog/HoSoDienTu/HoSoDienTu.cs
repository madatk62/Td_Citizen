namespace TD.CitizenAPI.Domain.Catalog;
public class HoSoDienTu : AuditableEntity, IAggregateRoot
{
    public string IDCongDan { get; set; }
    public string TaiKhoanTao { get; set; }
    public string TenHoSo { get;set; }
    public string MaHoSo { get; set; }
    public string? TenThuTuc { get; set; }
    public string? MaThuTuc { get; set; }
    public string? TenLinhVuc { get; set; }
    public string? MaLinhVuc { get; set; }
    public string? TenNhomHoSo { get; set; }
    public string? MaNhomHoSo { get; set; }
    public string? TenLoaiHoSo { get; set; }
    public string? MaLoaiHoSo { get; set; }
    public HoSoDienTu(string IDCongDan, string TaiKhoanTao, string TenHoSo, string MaHoSo, string? TenThuTuc, string? MaThuTuc, string? TenLinhVuc, string? MaLinhVuc, string? TenNhomHoSo, string? MaNhomHoSo, string? TenLoaiHoSo, string? MaLoaiHoSo)
    {
        this.IDCongDan = IDCongDan;
        this.TaiKhoanTao = TaiKhoanTao;
        this.TenHoSo = TenHoSo;
        this.MaHoSo = MaHoSo;
        if (TenThuTuc is not null) this.TenThuTuc = TenThuTuc;
        if (MaThuTuc is not null) this.MaThuTuc = MaThuTuc;
        if (TenLinhVuc is not null) this.TenLinhVuc = TenLinhVuc;
        if (MaLinhVuc is not null) this.MaLinhVuc = MaLinhVuc;
        if (TenNhomHoSo is not null) this.TenNhomHoSo = TenNhomHoSo;
        if (MaNhomHoSo is not null) this.MaNhomHoSo = MaNhomHoSo;
        if (TenLoaiHoSo is not null) this.TenLoaiHoSo = TenLoaiHoSo;
        if (MaLoaiHoSo is not null) this.MaLoaiHoSo = MaLoaiHoSo;
    }

    public HoSoDienTu Update(string? iDCongDan, string? taiKhoanTao,string? maHoSo, string? tenHoSo, string? maThuTuc, string? tenThuTuc, string? maLinhVuc, string? tenLinhVuc, string? tenNhomHoSo, string? maNhomHoSo, string? tenLoaiHoSo, string? maLoaiHoSo)
    {
        if (maHoSo is not null && MaHoSo?.Equals(maHoSo) is not true) MaHoSo = maHoSo;
        if (iDCongDan is not null && IDCongDan?.Equals(iDCongDan) is not true) IDCongDan = iDCongDan;
        if (taiKhoanTao is not null && TaiKhoanTao?.Equals(taiKhoanTao) is not true) TaiKhoanTao = taiKhoanTao;
        if (tenHoSo is not null && TenHoSo?.Equals(tenHoSo) is not true) TenHoSo = tenHoSo;
        if (tenThuTuc is not null && TenThuTuc?.Equals(tenThuTuc) is not true) TenThuTuc = tenThuTuc;
        if (maThuTuc is not null && MaThuTuc?.Equals(maThuTuc) is not true) MaThuTuc = maThuTuc;
        if (tenLinhVuc is not null && TenLinhVuc?.Equals(tenLinhVuc) is not true) TenLinhVuc = tenLinhVuc;
        if (maLinhVuc is not null && MaLinhVuc?.Equals(maLinhVuc) is not true) MaLinhVuc = maLinhVuc;
        if (tenNhomHoSo is not null && TenNhomHoSo?.Equals(tenNhomHoSo) is not true) TenNhomHoSo = tenNhomHoSo;
        if (maNhomHoSo is not null && MaNhomHoSo?.Equals(maNhomHoSo) is not true) MaNhomHoSo = maNhomHoSo;
        if (tenLoaiHoSo is not null && TenLoaiHoSo?.Equals(tenLoaiHoSo) is not true) TenLoaiHoSo = tenLoaiHoSo;
        if (maLoaiHoSo is not null && MaLoaiHoSo?.Equals(maLoaiHoSo) is not true) MaLoaiHoSo = maLoaiHoSo;
        return this;
    }

}
