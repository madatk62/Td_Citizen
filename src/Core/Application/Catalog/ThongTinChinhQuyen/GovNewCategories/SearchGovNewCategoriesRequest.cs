namespace TD.CitizenAPI.Application.Catalog.GovNewCategories;

public class SearchGovNewCategoriesRequest : PaginationFilter, IRequest<PaginationResponse<GovNewCategoryDto>>
{
}

public class HotlineCategoriesBySearchRequestSpec : EntitiesByPaginationFilterSpec<GovNewCategory, GovNewCategoryDto>
{
    public HotlineCategoriesBySearchRequestSpec(SearchGovNewCategoriesRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class SearchHotlineCategoriesRequestHandler : IRequestHandler<SearchGovNewCategoriesRequest, PaginationResponse<GovNewCategoryDto>>
{
    private readonly IReadRepository<GovNewCategory> _repository;

    public SearchHotlineCategoriesRequestHandler(IReadRepository<GovNewCategory> repository) => _repository = repository;

    public async Task<PaginationResponse<GovNewCategoryDto>> Handle(SearchGovNewCategoriesRequest request, CancellationToken cancellationToken)
    {
        var spec = new HotlineCategoriesBySearchRequestSpec(request);

        var list = await _repository.ListAsync(spec, cancellationToken);
        int count = await _repository.CountAsync(spec, cancellationToken);

        return new PaginationResponse<GovNewCategoryDto>(list, count, request.PageNumber, request.PageSize);
    }
}