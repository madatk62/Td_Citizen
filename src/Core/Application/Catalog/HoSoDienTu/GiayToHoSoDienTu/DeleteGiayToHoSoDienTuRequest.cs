namespace TD.CitizenAPI.Application.Catalog.HoSoDienTus;

public class DeleteGiayToHoSoDienTuRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }

    public DeleteGiayToHoSoDienTuRequest(Guid id) => Id = id;
}

public class DeleteGiayToHoSoDienTuRequestHandler : IRequestHandler<DeleteGiayToHoSoDienTuRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<GiayToHoSoDienTu> _giayToHoSoDienTuRepo;
    private readonly IStringLocalizer<DeleteGiayToHoSoDienTuRequestHandler> _localizer;

    public DeleteGiayToHoSoDienTuRequestHandler(IRepositoryWithEvents<GiayToHoSoDienTu> giayToHoSoDienTuRepo, IStringLocalizer<DeleteGiayToHoSoDienTuRequestHandler> localizer) =>
        (_giayToHoSoDienTuRepo, _localizer) = (giayToHoSoDienTuRepo, localizer);

    public async Task<Result<Guid>> Handle(DeleteGiayToHoSoDienTuRequest request, CancellationToken cancellationToken)
    {
        var item = await _giayToHoSoDienTuRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(_localizer["giayToHoSoDienTu.notfound"]);

        await _giayToHoSoDienTuRepo.DeleteAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}