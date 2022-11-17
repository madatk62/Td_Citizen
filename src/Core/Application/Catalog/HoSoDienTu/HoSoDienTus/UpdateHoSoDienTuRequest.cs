namespace TD.CitizenAPI.Application.Catalog.HoSoDienTus;

public class UpdateHoSoDienTuRequest : IRequest<Result<Guid>>
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
    public string? TenNhomHoSo { get; set; }
    public string? MaNhomHoSo { get; set; }
    public string? TenLoaiHoSo { get; set; }
    public string? MaLoaiHoSo { get; set; }
}

public class UpdateHoSoDienTuRequestValidator : CustomValidator<UpdateHoSoDienTuRequest>
{
    //public UpdateHoSoDienTuRequestValidator(IRepository<HoSoDienTu> repository, IStringLocalizer<UpdateHoSoDienTuRequestValidator> localizer) =>
    //    RuleFor(p => p.IDCongDan)
    //        .NotEmpty();
}

public class UpdateHoSoDienTuRequestHandler : IRequestHandler<UpdateHoSoDienTuRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<HoSoDienTu> _repository;
    private readonly IStringLocalizer<UpdateHoSoDienTuRequestHandler> _localizer;

    public UpdateHoSoDienTuRequestHandler(IRepositoryWithEvents<HoSoDienTu> repository, IStringLocalizer<UpdateHoSoDienTuRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Result<Guid>> Handle(UpdateHoSoDienTuRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(string.Format(_localizer["hosodientu.notfound"], request.Id));

        item.Update(request.IDCongDan, request.TaiKhoanTao, request.MaHoSo, request.TenHoSo, request.MaThuTuc, request.TenThuTuc,request.MaLinhVuc, request.TenLinhVuc, request.TenNhomHoSo,request.MaNhomHoSo,request.TenLoaiHoSo,request.MaLoaiHoSo);

        await _repository.UpdateAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}