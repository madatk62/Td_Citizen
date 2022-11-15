namespace TD.CitizenAPI.Application.Catalog.NhomHoSoDienTus;

public class UpdateNhomHoSoDienTuRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string Ten { get; set; }
    public string Ma { get; set; }
    public int? ThuTu { get; set; } = 0;
    public string? IDCongDan { get; set; }
}

public class UpdateNhomHoSoDienTuRequestValidator : CustomValidator<UpdateNhomHoSoDienTuRequest>
{
    //public UpdateNhomHoSoDienTuRequestValidator(IRepository<NhomHoSoDienTu> repository, IStringLocalizer<UpdateNhomHoSoDienTuRequestValidator> localizer) =>
    //    RuleFor(p => p.IDCongDan)
    //        .NotEmpty();
}

public class UpdateNhomHoSoDienTuRequestHandler : IRequestHandler<UpdateNhomHoSoDienTuRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<NhomHoSoDienTu> _repository;
    private readonly IStringLocalizer<UpdateNhomHoSoDienTuRequestHandler> _localizer;

    public UpdateNhomHoSoDienTuRequestHandler(IRepositoryWithEvents<NhomHoSoDienTu> repository, IStringLocalizer<UpdateNhomHoSoDienTuRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Result<Guid>> Handle(UpdateNhomHoSoDienTuRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(string.Format(_localizer["hosodientu.notfound"], request.Id));

        item.Update(request.Ten, request.Ma, request.ThuTu, request.IDCongDan);

        await _repository.UpdateAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}