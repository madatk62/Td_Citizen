namespace TD.CitizenAPI.Application.Catalog.HoSoDienTus;

public class DeleteHoSoDienTuRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }

    public DeleteHoSoDienTuRequest(Guid id) => Id = id;
}

public class DeleteHoSoDienTuRequestHandler : IRequestHandler<DeleteHoSoDienTuRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<HoSoDienTu> _hoSoDienTuRepo;
    private readonly IReadRepository<Hotline> _hotlineRepo;
    private readonly IStringLocalizer<DeleteHoSoDienTuRequestHandler> _localizer;

    public DeleteHoSoDienTuRequestHandler(IRepositoryWithEvents<HoSoDienTu> hoSoDienTuRepo, IReadRepository<Hotline> hotlineRepo, IStringLocalizer<DeleteHoSoDienTuRequestHandler> localizer) =>
        (_hoSoDienTuRepo, _hotlineRepo, _localizer) = (hoSoDienTuRepo, hotlineRepo, localizer);

    public async Task<Result<Guid>> Handle(DeleteHoSoDienTuRequest request, CancellationToken cancellationToken)
    {
        var item = await _hoSoDienTuRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(_localizer["hoSoDienTu.notfound"]);

        await _hoSoDienTuRepo.DeleteAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}