namespace TD.CitizenAPI.Application.Catalog.HoSoDienTus;

public partial class CreateGiayToHoSoDienTuRequest : IRequest<Result<Guid>>
{

    public string IDCongDan { get; set; }
    public string HoSoDienTuID { get; set; }
    public string? MaHoSoDienTu { get; set; }
    public string? TenGiayTo { get; set; }
    public string? MaGiayTo { get; set; }
    public string? DinhKem { get; set; }
    public string? SoGiayTo { get; set; }
    public string? LoaiGiayToID { get; set; }
    public string? TenLoaiGiayTo { get; set; }
    public string? NhomGiayToID { get; set; }
    public string? TenNhomGiayTo { get; set; }
    public string? GiayToCaNhanID { get; set; }
}

public class CreateGiayToHoSoDienTuRequestValidator : CustomValidator<CreateGiayToHoSoDienTuRequest>
{
    public CreateGiayToHoSoDienTuRequestValidator(IReadRepository<GiayToHoSoDienTu> repository, IStringLocalizer<CreateGiayToHoSoDienTuRequestValidator> localizer) =>
        RuleFor(p => p.TenGiayTo).NotEmpty();
}

public class CreateGiayToHoSoDienTuRequestHandler : IRequestHandler<CreateGiayToHoSoDienTuRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<GiayToHoSoDienTu> _repository;

    public CreateGiayToHoSoDienTuRequestHandler(IRepositoryWithEvents<GiayToHoSoDienTu> repository) => _repository = repository;

    public async Task<Result<Guid>> Handle(CreateGiayToHoSoDienTuRequest request, CancellationToken cancellationToken)
    {
        var item = new GiayToHoSoDienTu(request.IDCongDan, request.HoSoDienTuID, request.MaHoSoDienTu, request.TenGiayTo, request.MaGiayTo, request.DinhKem, request.LoaiGiayToID, request.TenLoaiGiayTo, request.NhomGiayToID, request.TenNhomGiayTo, request.SoGiayTo, request.GiayToCaNhanID);
        await _repository.AddAsync(item, cancellationToken);
        return Result<Guid>.Success(item.Id);
    }
}