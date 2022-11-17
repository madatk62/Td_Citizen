namespace TD.CitizenAPI.Domain.Catalog;
public class GiayToHoSoDienTu : AuditableEntity, IAggregateRoot
{
    public string? IDCongDan { get; set; }
    public string HoSoDienTuID { get; set; }
    public string? MaHoSoDienTu { get; set; }
    public string TenGiayTo { get; set; }
    public string? GiayToCaNhanID { get; set; }
    public string MaGiayTo { get; set; }
    public string? DinhKem { get; set; }
    public string? SoGiayTo { get; set; }
    public string? LoaiGiayToID { get; set; }
    public string? TenLoaiGiayTo { get; set; }
    public string? NhomGiayToID { get; set; }
    public string? TenNhomGiayTo { get; set; }
    public GiayToHoSoDienTu (string iDCongDan, string hoSoDienTuID, string? maHoSoDienTu, string? tenGiayTo, string maGiayTo, string? dinhKem, string? loaiGiayToID, string? tenLoaiGiayTo, string? nhomGiayToID, string? tenNhomGiayTo, string? soGiayTo, string? giayToCaNhanID)
    {
        IDCongDan = iDCongDan;
        HoSoDienTuID = hoSoDienTuID;
        if (maHoSoDienTu is not null) MaHoSoDienTu = maHoSoDienTu;
        TenGiayTo = tenGiayTo;
        MaGiayTo = maGiayTo;
        if (loaiGiayToID is not null) LoaiGiayToID = loaiGiayToID;
        if (tenLoaiGiayTo is not null) TenLoaiGiayTo = tenLoaiGiayTo;
        if (nhomGiayToID is not null) NhomGiayToID = nhomGiayToID;
        if (giayToCaNhanID is not null) GiayToCaNhanID = giayToCaNhanID;
        if (tenNhomGiayTo is not null) TenNhomGiayTo = tenNhomGiayTo;
        if (dinhKem is not null) DinhKem = dinhKem;
        if (soGiayTo is not null) SoGiayTo = soGiayTo;
    }

    public GiayToHoSoDienTu Update(string? hoSoDienTuID, string? maHoSoDienTu, string? tenGiayTo, string? maGiayTo, string? dinhKem, string? loaiGiayToID, string? tenLoaiGiayTo, string? nhomGiayToID, string? tenNhomGiayTo, string? soGiayTo, string? giayToCaNhanID)
    {
        if (hoSoDienTuID is not null && HoSoDienTuID?.Equals(hoSoDienTuID) is not true) HoSoDienTuID = hoSoDienTuID;
        if (maHoSoDienTu is not null && MaHoSoDienTu?.Equals(maHoSoDienTu) is not true) MaHoSoDienTu = maHoSoDienTu;
        if (tenGiayTo is not null && TenGiayTo?.Equals(tenGiayTo) is not true) TenGiayTo = tenGiayTo;
        if (maGiayTo is not null && MaGiayTo?.Equals(maGiayTo) is not true) MaGiayTo = maGiayTo;
        if (dinhKem is not null && DinhKem?.Equals(dinhKem) is not true) DinhKem = dinhKem;
        if (loaiGiayToID is not null && LoaiGiayToID?.Equals(loaiGiayToID) is not true) LoaiGiayToID = loaiGiayToID;
        if (tenLoaiGiayTo is not null && TenLoaiGiayTo?.Equals(tenLoaiGiayTo) is not true) TenLoaiGiayTo = tenLoaiGiayTo;
        if (nhomGiayToID is not null && NhomGiayToID?.Equals(nhomGiayToID) is not true) NhomGiayToID = nhomGiayToID;
        if (tenNhomGiayTo is not null && TenNhomGiayTo?.Equals(tenNhomGiayTo) is not true) TenNhomGiayTo = tenNhomGiayTo;
        if (soGiayTo is not null && SoGiayTo?.Equals(soGiayTo) is not true) SoGiayTo = soGiayTo;
        if (giayToCaNhanID is not null && GiayToCaNhanID?.Equals(giayToCaNhanID) is not true) GiayToCaNhanID = giayToCaNhanID;
        return this;
    }

}
