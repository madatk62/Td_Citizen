namespace TD.CitizenAPI.Application.Catalog.ThoiGianThueNhas;

public class SearchThoiGianThueNhasRequest : PaginationFilter, IRequest<PaginationResponse<ThoiGianThueNhaDto>>
{
}

public class ThoiGianThueNhasBySearchRequestSpec : EntitiesByPaginationFilterSpec<ThoiGianThueNha, ThoiGianThueNhaDto>
{
    public ThoiGianThueNhasBySearchRequestSpec(SearchThoiGianThueNhasRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class SearchThoiGianThueNhasRequestHandler : IRequestHandler<SearchThoiGianThueNhasRequest, PaginationResponse<ThoiGianThueNhaDto>>
{
    private readonly IReadRepository<ThoiGianThueNha> _repository;

    public SearchThoiGianThueNhasRequestHandler(IReadRepository<ThoiGianThueNha> repository) => _repository = repository;

    public async Task<PaginationResponse<ThoiGianThueNhaDto>> Handle(SearchThoiGianThueNhasRequest request, CancellationToken cancellationToken)
    {
        var spec = new ThoiGianThueNhasBySearchRequestSpec(request);

        var list = await _repository.ListAsync(spec, cancellationToken);
        int count = await _repository.CountAsync(spec, cancellationToken);

        return new PaginationResponse<ThoiGianThueNhaDto>(list, count, request.PageNumber, request.PageSize);
    }
}