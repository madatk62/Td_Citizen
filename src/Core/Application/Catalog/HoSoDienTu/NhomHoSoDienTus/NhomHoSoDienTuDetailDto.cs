namespace TD.CitizenAPI.Application.Catalog.NhomHoSoDienTus;
public class NhomHoSoDienTuDetailDto : IDto
{
    public Guid Id { get; set; }
    public string Ten { get; set; }
    public string Ma { get; set; }
    public int? ThuTu { get; set; } = 0;
}
