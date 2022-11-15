namespace TD.CitizenAPI.Application.Catalog.AppConfigs;

public class SearchAppConfigsRequest : PaginationFilter, IRequest<PaginationResponse<AppConfigDto>>
{
}

public class MarketCategoriesBySearchRequestSpec : EntitiesByPaginationFilterSpec<AppConfig, AppConfigDto>
{
    public MarketCategoriesBySearchRequestSpec(SearchAppConfigsRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Key, !request.HasOrderBy());
}

public class SearchMarketCategoriesRequestHandler : IRequestHandler<SearchAppConfigsRequest, PaginationResponse<AppConfigDto>>
{
    private readonly IReadRepository<AppConfig> _repository;

    public SearchMarketCategoriesRequestHandler(IReadRepository<AppConfig> repository) => _repository = repository;

    public async Task<PaginationResponse<AppConfigDto>> Handle(SearchAppConfigsRequest request, CancellationToken cancellationToken)
    {
        var spec = new MarketCategoriesBySearchRequestSpec(request);

        var list = await _repository.ListAsync(spec, cancellationToken);
        int count = await _repository.CountAsync(spec, cancellationToken);

        return new PaginationResponse<AppConfigDto>(list, count, request.PageNumber, request.PageSize);
    }
}