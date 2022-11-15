namespace TD.CitizenAPI.Domain.Catalog;
public class NhomHoSoDienTu : AuditableEntity, IAggregateRoot
{
    public string Ten { get; set; }
    public string Ma { get; set; }
    public int? ThuTu { get; set; } = 0;
    public string? IDCongDan { get; set; }

    public NhomHoSoDienTu(string ten, string ma, int? thuTu, string? iDCongDan)
    {
        Ten = ten;
        Ma = ma;
        if (thuTu is not null) { ThuTu = thuTu.Value; } else { ThuTu = 0; }
        IDCongDan = iDCongDan;
    }

    public NhomHoSoDienTu Update(string? ten, string? ma, int? thuTu, string? iDCongDan)
    {
        if (ten is not null) Ten = ten;
        if (ma is not null && Ma?.Equals(ma) is not true) Ma = ma;
        if (thuTu is not null) ThuTu = thuTu;
        if (iDCongDan is not null && IDCongDan?.Equals(iDCongDan) is not true) Ma = iDCongDan;
        return this;
    }

}
