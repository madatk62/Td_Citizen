namespace TD.CitizenAPI.Application.Catalog.LoaiHoSoDienTus;

public partial class CreateLoaiHoSoDienTuRequest : IRequest<Result<Guid>>
{

    public string Ten { get; set; } = default!;
    public string Ma { get; set; } = default!;
    public int? ThuTu { get; set; }
    public string? IDCongDan { get; set; }

}

public class CreateLoaiHoSoDienTuRequestValidator : CustomValidator<CreateLoaiHoSoDienTuRequest>
{
    public CreateLoaiHoSoDienTuRequestValidator(IReadRepository<LoaiHoSoDienTu> repository, IStringLocalizer<CreateLoaiHoSoDienTuRequestValidator> localizer) =>
        RuleFor(p => p.Ten).NotEmpty().WithMessage("Tên loại hồ sơ không được để trống");
    // .MustAsync(async(tenHoSo,_) => !await repository.ExistsWithIdentityNumberAsync(identityNumber!))
    //     .WithMessage((_, identityNumber) => string.Format(localizer["Số CCCD/CMND {0} đã tồn tại trong hệ thống."], identityNumber));
}

public class CreateLoaiHoSoDienTuRequestHandler : IRequestHandler<CreateLoaiHoSoDienTuRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<LoaiHoSoDienTu> _repository;

    public CreateLoaiHoSoDienTuRequestHandler(IRepositoryWithEvents<LoaiHoSoDienTu> repository) => _repository = repository;

    public async Task<Result<Guid>> Handle(CreateLoaiHoSoDienTuRequest request, CancellationToken cancellationToken)
    {
        var item = new LoaiHoSoDienTu(request.Ten, request.Ma, request.ThuTu, request.IDCongDan);
        await _repository.AddAsync(item, cancellationToken);
        return Result<Guid>.Success(item.Id);
    }
}