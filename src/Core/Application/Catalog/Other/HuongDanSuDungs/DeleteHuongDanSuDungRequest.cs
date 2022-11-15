
namespace TD.CitizenAPI.Application.Catalog.HuongDanSuDungs;

public class DeleteHuongDanSuDungRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }

    public DeleteHuongDanSuDungRequest(Guid id) => Id = id;
}

public class DeleteHuongDanSuDungRequestHandler : IRequestHandler<DeleteHuongDanSuDungRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<HuongDanSuDung> _huongDanSuDungRepo;
    private readonly IStringLocalizer<DeleteHuongDanSuDungRequestHandler> _localizer;

    public DeleteHuongDanSuDungRequestHandler(IRepositoryWithEvents<HuongDanSuDung> huongDanSuDungRepo,  IStringLocalizer<DeleteHuongDanSuDungRequestHandler> localizer) =>
        (_huongDanSuDungRepo,  _localizer) = (huongDanSuDungRepo,  localizer);

    public async Task<Result<Guid>> Handle(DeleteHuongDanSuDungRequest request, CancellationToken cancellationToken)
    {


        var item = await _huongDanSuDungRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(_localizer["HuongDanSuDung.notfound"]);

        await _huongDanSuDungRepo.DeleteAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}