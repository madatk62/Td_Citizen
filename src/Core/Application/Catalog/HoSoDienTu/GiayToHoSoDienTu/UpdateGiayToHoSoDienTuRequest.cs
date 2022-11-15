namespace TD.CitizenAPI.Application.Catalog.HoSoDienTus;

public class UpdateGiayToHoSoDienTuRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
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

public class UpdateGiayToHoSoDienTuRequestValidator : CustomValidator<UpdateGiayToHoSoDienTuRequest>
{
    //public UpdateGiayToHoSoDienTuRequestValidator(IRepository<GiayToHoSoDienTu> repository, IStringLocalizer<UpdateGiayToHoSoDienTuRequestValidator> localizer) =>
    //    RuleFor(p => p.HoSoDienTuID)
    //        .NotEmpty();
}

public class UpdateGiayToHoSoDienTuRequestHandler : IRequestHandler<UpdateGiayToHoSoDienTuRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<GiayToHoSoDienTu> _repository;
    private readonly IStringLocalizer<UpdateGiayToHoSoDienTuRequestHandler> _localizer;

    public UpdateGiayToHoSoDienTuRequestHandler(IRepositoryWithEvents<GiayToHoSoDienTu> repository, IStringLocalizer<UpdateGiayToHoSoDienTuRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Result<Guid>> Handle(UpdateGiayToHoSoDienTuRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(string.Format(_localizer["hosodientu.notfound"], request.Id));

        item.Update(request.HoSoDienTuID, request.MaHoSoDienTu, request.TenGiayTo, request.MaGiayTo, request.DinhKem, request.LoaiGiayToID, request.TenLoaiGiayTo, request.NhomGiayToID, request.TenNhomGiayTo, request.SoGiayTo);

        await _repository.UpdateAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}