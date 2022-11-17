namespace TD.CitizenAPI.Application.Catalog.HoSoDienTus;

public partial class CreateHoSoDienTuRequest : IRequest<Result<Guid>>
{
    public string IDCongDan { get; set; }
    public string TaiKhoanTao { get; set; }
    public string TenHoSo { get; set; } = default!;
    public string MaHoSo { get; set; } = default!;
    public string? TenThuTuc { get; set; }
    public string? MaThuTuc { get; set; }
    public string? TenLinhVuc { get; set; }
    public string? MaLinhVuc { get; set; }
    public string? TenNhomHoSo { get; set; }
    public string? MaNhomHoSo { get; set; }
    public string? TenLoaiHoSo { get; set; }
    public string? MaLoaiHoSo { get; set; }
}

public class CreateHoSoDienTuRequestValidator : CustomValidator<CreateHoSoDienTuRequest>
{
    public CreateHoSoDienTuRequestValidator(IReadRepository<HoSoDienTu> repository, IStringLocalizer<CreateHoSoDienTuRequestValidator> localizer) =>
        RuleFor(p => p.TenHoSo).NotEmpty();
}

public class CreateHoSoDienTuRequestHandler : IRequestHandler<CreateHoSoDienTuRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<HoSoDienTu> _repository;

    public CreateHoSoDienTuRequestHandler(IRepositoryWithEvents<HoSoDienTu> repository) => _repository = repository;

    public async Task<Result<Guid>> Handle(CreateHoSoDienTuRequest request, CancellationToken cancellationToken)
    {
        var item = new HoSoDienTu(request.IDCongDan, request.TaiKhoanTao, request.TenHoSo, request.MaHoSo, request.TenThuTuc, request.MaThuTuc, request.TenLinhVuc, request.MaLinhVuc, request.TenNhomHoSo, request.MaNhomHoSo, request.TenLoaiHoSo, request.MaLoaiHoSo);
        await _repository.AddAsync(item, cancellationToken);
        return Result<Guid>.Success(item.Id);
    }
}