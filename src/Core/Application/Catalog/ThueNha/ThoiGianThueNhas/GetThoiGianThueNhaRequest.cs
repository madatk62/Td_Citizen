namespace TD.CitizenAPI.Application.Catalog.ThoiGianThueNhas;

public class GetThoiGianThueNhaRequest : IRequest<Result<ThoiGianThueNhaDetailsDto>>
{
    public Guid Id { get; set; }

    public GetThoiGianThueNhaRequest(Guid id) => Id = id;
}

public class ThoiGianThueNhaByIdSpec : Specification<ThoiGianThueNha, ThoiGianThueNhaDetailsDto>, ISingleResultSpecification
{
    public ThoiGianThueNhaByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetThoiGianThueNhaRequestHandler : IRequestHandler<GetThoiGianThueNhaRequest, Result<ThoiGianThueNhaDetailsDto>>
{
    private readonly IRepository<ThoiGianThueNha> _repository;
    private readonly IStringLocalizer<GetThoiGianThueNhaRequestHandler> _localizer;

    public GetThoiGianThueNhaRequestHandler(IRepository<ThoiGianThueNha> repository, IStringLocalizer<GetThoiGianThueNhaRequestHandler> localizer) => (_repository, _localizer) = (repository, localizer);

    public async Task<Result<ThoiGianThueNhaDetailsDto>> Handle(GetThoiGianThueNhaRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetBySpecAsync(
            (ISpecification<ThoiGianThueNha, ThoiGianThueNhaDetailsDto>)new ThoiGianThueNhaByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["ThoiGianThueNha.notfound"], request.Id));
        return Result<ThoiGianThueNhaDetailsDto>.Success(item);

    }
}