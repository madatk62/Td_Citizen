namespace TD.CitizenAPI.Application.Catalog.LoaiHoSoDienTus;

public class UpdateLoaiHoSoDienTuRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string Ten { get; set; }
    public string Ma { get; set; }
    public int? ThuTu { get; set; } = 0;
    public string? IDCongDan { get; set; }  
}

public class UpdateLoaiHoSoDienTuRequestValidator : CustomValidator<UpdateLoaiHoSoDienTuRequest>
{
    //public UpdateLoaiHoSoDienTuRequestValidator(IRepository<LoaiHoSoDienTu> repository, IStringLocalizer<UpdateLoaiHoSoDienTuRequestValidator> localizer) =>
    //    RuleFor(p => p.IDCongDan)
    //        .NotEmpty();
}

public class UpdateLoaiHoSoDienTuRequestHandler : IRequestHandler<UpdateLoaiHoSoDienTuRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<LoaiHoSoDienTu> _repository;
    private readonly IStringLocalizer<UpdateLoaiHoSoDienTuRequestHandler> _localizer;

    public UpdateLoaiHoSoDienTuRequestHandler(IRepositoryWithEvents<LoaiHoSoDienTu> repository, IStringLocalizer<UpdateLoaiHoSoDienTuRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Result<Guid>> Handle(UpdateLoaiHoSoDienTuRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(string.Format(_localizer["loaihosodientu.notfound"], request.Id));

        item.Update(request.Ten, request.Ma, request.ThuTu,request.IDCongDan);

        await _repository.UpdateAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}