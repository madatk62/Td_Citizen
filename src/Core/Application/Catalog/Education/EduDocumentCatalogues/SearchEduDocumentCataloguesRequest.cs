namespace TD.CitizenAPI.Application.Catalog.EduDocumentCatalogues;

public class SearchEduDocumentCataloguesRequest : PaginationFilter, IRequest<PaginationResponse<EduDocumentCatalogueDto>>
{
}

public class HotlineCategoriesBySearchRequestSpec : EntitiesByPaginationFilterSpec<EduDocumentCatalogue, EduDocumentCatalogueDto>
{
    public HotlineCategoriesBySearchRequestSpec(SearchEduDocumentCataloguesRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class SearchHotlineCategoriesRequestHandler : IRequestHandler<SearchEduDocumentCataloguesRequest, PaginationResponse<EduDocumentCatalogueDto>>
{
    private readonly IReadRepository<EduDocumentCatalogue> _repository;

    public SearchHotlineCategoriesRequestHandler(IReadRepository<EduDocumentCatalogue> repository) => _repository = repository;

    public async Task<PaginationResponse<EduDocumentCatalogueDto>> Handle(SearchEduDocumentCataloguesRequest request, CancellationToken cancellationToken)
    {
        var spec = new HotlineCategoriesBySearchRequestSpec(request);

        var list = await _repository.ListAsync(spec, cancellationToken);
        int count = await _repository.CountAsync(spec, cancellationToken);

        return new PaginationResponse<EduDocumentCatalogueDto>(list, count, request.PageNumber, request.PageSize);
    }
}