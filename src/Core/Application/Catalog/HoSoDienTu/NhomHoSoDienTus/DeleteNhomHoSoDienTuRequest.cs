namespace TD.CitizenAPI.Application.Catalog.NhomHoSoDienTus;

public class DeleteNhomHoSoDienTuRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }

    public DeleteNhomHoSoDienTuRequest(Guid id) => Id = id;
}

public class DeleteNhomHoSoDienTuRequestHandler : IRequestHandler<DeleteNhomHoSoDienTuRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<NhomHoSoDienTu> _nhomHoSoDienTuRepo;
    private readonly IReadRepository<Hotline> _hotlineRepo;
    private readonly IStringLocalizer<DeleteNhomHoSoDienTuRequestHandler> _localizer;

    public DeleteNhomHoSoDienTuRequestHandler(IRepositoryWithEvents<NhomHoSoDienTu> nhomHoSoDienTuRepo, IReadRepository<Hotline> hotlineRepo, IStringLocalizer<DeleteNhomHoSoDienTuRequestHandler> localizer) =>
        (_nhomHoSoDienTuRepo, _hotlineRepo, _localizer) = (nhomHoSoDienTuRepo, hotlineRepo, localizer);

    public async Task<Result<Guid>> Handle(DeleteNhomHoSoDienTuRequest request, CancellationToken cancellationToken)
    {
        var item = await _nhomHoSoDienTuRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(_localizer["nhomHoSoDienTu.notfound"]);

        await _nhomHoSoDienTuRepo.DeleteAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}