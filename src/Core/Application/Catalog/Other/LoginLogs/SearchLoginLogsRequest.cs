namespace TD.CitizenAPI.Application.Catalog.LoginLogs;

public class SearchLoginLogsRequest : PaginationFilter, IRequest<PaginationResponse<LoginLogDto>>
{
    public string? UserName { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}

public class MarketCategoriesBySearchRequestSpec : EntitiesByPaginationFilterSpec<LoginLog, LoginLogDto>
{
    public MarketCategoriesBySearchRequestSpec(SearchLoginLogsRequest request)
        : base(request) =>
        Query.Where(p => p.UserName == request.UserName, !string.IsNullOrEmpty(request.UserName))
            .Where(p => p.CreatedOn >= request.FromDate, request.FromDate.HasValue)
            .Where(p => p.CreatedOn <= request.ToDate, request.ToDate.HasValue);
}

public class SearchMarketCategoriesRequestHandler : IRequestHandler<SearchLoginLogsRequest, PaginationResponse<LoginLogDto>>
{
    private readonly IReadRepository<LoginLog> _repository;

    public SearchMarketCategoriesRequestHandler(IReadRepository<LoginLog> repository) => _repository = repository;

    public async Task<PaginationResponse<LoginLogDto>> Handle(SearchLoginLogsRequest request, CancellationToken cancellationToken)
    {
        var spec = new MarketCategoriesBySearchRequestSpec(request);

        var list = await _repository.ListAsync(spec, cancellationToken);
        int count = await _repository.CountAsync(spec, cancellationToken);

        return new PaginationResponse<LoginLogDto>(list, count, request.PageNumber, request.PageSize);
    }
}