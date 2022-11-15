namespace TD.CitizenAPI.Application.Catalog.NhomHoSoDienTus;

public partial class CreateNhomHoSoDienTuRequest : IRequest<Result<Guid>>
{

    public string Ten { get; set; } = default!;
    public string Ma { get; set; } = default!;
    public int? ThuTu { get; set; }
    public string? IDCongDan { get; set; }

}

public class CreateNhomHoSoDienTuRequestValidator : CustomValidator<CreateNhomHoSoDienTuRequest>
{
    public CreateNhomHoSoDienTuRequestValidator(IReadRepository<NhomHoSoDienTu> repository, IStringLocalizer<CreateNhomHoSoDienTuRequestValidator> localizer) =>
        RuleFor(p => p.Ten).NotEmpty().WithMessage("Tên nhóm hồ sơ không được để trống");
        // .MustAsync(async(tenHoSo,_) => !await repository.ExistsWithIdentityNumberAsync(identityNumber!))
        //     .WithMessage((_, identityNumber) => string.Format(localizer["Số CCCD/CMND {0} đã tồn tại trong hệ thống."], identityNumber));
}

public class CreateNhomHoSoDienTuRequestHandler : IRequestHandler<CreateNhomHoSoDienTuRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<NhomHoSoDienTu> _repository;

    public CreateNhomHoSoDienTuRequestHandler(IRepositoryWithEvents<NhomHoSoDienTu> repository) => _repository = repository;

    public async Task<Result<Guid>> Handle(CreateNhomHoSoDienTuRequest request, CancellationToken cancellationToken)
    {
        var item = new NhomHoSoDienTu(request.Ten, request.Ma, request.ThuTu, request.IDCongDan);
        await _repository.AddAsync(item, cancellationToken);
        return Result<Guid>.Success(item.Id);
    }
}