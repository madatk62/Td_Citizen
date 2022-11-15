namespace TD.CitizenAPI.Application.Catalog.HuongDanSuDungs;

public class GetHuongDanSuDungRequest : IRequest<Result<HuongDanSuDungDetailsDto>>
{
    public Guid Id { get; set; }

    public GetHuongDanSuDungRequest(Guid id) => Id = id;
}

public class HuongDanSuDungByIdSpec : Specification<HuongDanSuDung, HuongDanSuDungDetailsDto>, ISingleResultSpecification
{
    public HuongDanSuDungByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetHuongDanSuDungRequestHandler : IRequestHandler<GetHuongDanSuDungRequest, Result<HuongDanSuDungDetailsDto>>
{
    private readonly IRepository<HuongDanSuDung> _repository;
    private readonly IStringLocalizer<GetHuongDanSuDungRequestHandler> _localizer;

    public GetHuongDanSuDungRequestHandler(IRepository<HuongDanSuDung> repository, IStringLocalizer<GetHuongDanSuDungRequestHandler> localizer) => (_repository, _localizer) = (repository, localizer);

    public async Task<Result<HuongDanSuDungDetailsDto>> Handle(GetHuongDanSuDungRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetBySpecAsync(
            (ISpecification<HuongDanSuDung, HuongDanSuDungDetailsDto>)new HuongDanSuDungByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["HuongDanSuDung.notfound"], request.Id));
        return Result<HuongDanSuDungDetailsDto>.Success(item);

    }
}