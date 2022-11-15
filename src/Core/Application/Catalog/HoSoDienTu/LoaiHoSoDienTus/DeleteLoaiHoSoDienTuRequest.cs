namespace TD.CitizenAPI.Application.Catalog.LoaiHoSoDienTus;

public class DeleteLoaiHoSoDienTuRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }

    public DeleteLoaiHoSoDienTuRequest(Guid id) => Id = id;
}

public class DeleteLoaiHoSoDienTuRequestHandler : IRequestHandler<DeleteLoaiHoSoDienTuRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<LoaiHoSoDienTu> _loaiHoSoDienTuRepo;
    private readonly IReadRepository<Hotline> _hotlineRepo;
    private readonly IStringLocalizer<DeleteLoaiHoSoDienTuRequestHandler> _localizer;

    public DeleteLoaiHoSoDienTuRequestHandler(IRepositoryWithEvents<LoaiHoSoDienTu> loaiHoSoDienTuRepo, IReadRepository<Hotline> hotlineRepo, IStringLocalizer<DeleteLoaiHoSoDienTuRequestHandler> localizer) =>
        (_loaiHoSoDienTuRepo, _hotlineRepo, _localizer) = (loaiHoSoDienTuRepo, hotlineRepo, localizer);

    public async Task<Result<Guid>> Handle(DeleteLoaiHoSoDienTuRequest request, CancellationToken cancellationToken)
    {
        var item = await _loaiHoSoDienTuRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(_localizer["loaiHoSoDienTu.notfound"]);

        await _loaiHoSoDienTuRepo.DeleteAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}