namespace TD.CitizenAPI.Application.Catalog.HuongDanSuDungs;

public class SearchHuongDanSuDungsRequest : PaginationFilter, IRequest<PaginationResponse<HuongDanSuDungDto>>
{
}

public class MarketCategoriesBySearchRequestSpec : EntitiesByPaginationFilterSpec<HuongDanSuDung, HuongDanSuDungDto>
{
    public MarketCategoriesBySearchRequestSpec(SearchHuongDanSuDungsRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class SearchMarketCategoriesRequestHandler : IRequestHandler<SearchHuongDanSuDungsRequest, PaginationResponse<HuongDanSuDungDto>>
{
    private readonly IReadRepository<HuongDanSuDung> _repository;

    public SearchMarketCategoriesRequestHandler(IReadRepository<HuongDanSuDung> repository) => _repository = repository;

    public async Task<PaginationResponse<HuongDanSuDungDto>> Handle(SearchHuongDanSuDungsRequest request, CancellationToken cancellationToken)
    {
        var spec = new MarketCategoriesBySearchRequestSpec(request);

        var list = await _repository.ListAsync(spec, cancellationToken);
        int count = await _repository.CountAsync(spec, cancellationToken);

        return new PaginationResponse<HuongDanSuDungDto>(list, count, request.PageNumber, request.PageSize);
    }
}