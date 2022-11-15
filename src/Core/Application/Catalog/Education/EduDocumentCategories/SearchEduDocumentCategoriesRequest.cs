namespace TD.CitizenAPI.Application.Catalog.EduDocumentCategories;

public class SearchEduDocumentCategoriesRequest : PaginationFilter, IRequest<PaginationResponse<EduDocumentCategoryDto>>
{
    public Guid? EduDocumentCatalogueId { get; set; }

}

public class HotlineCategoriesBySearchRequestSpec : EntitiesByPaginationFilterSpec<EduDocumentCategory, EduDocumentCategoryDto>
{
    public HotlineCategoriesBySearchRequestSpec(SearchEduDocumentCategoriesRequest request)
        : base(request) =>
        Query.Include(p => p.EduDocumentCatalogue)
        .Where(p => p.EduDocumentCatalogueId.Equals(request.EduDocumentCatalogueId!.Value), request.EduDocumentCatalogueId.HasValue)
        .OrderBy(c => c.Name, !request.HasOrderBy());
}

public class SearchHotlineCategoriesRequestHandler : IRequestHandler<SearchEduDocumentCategoriesRequest, PaginationResponse<EduDocumentCategoryDto>>
{
    private readonly IReadRepository<EduDocumentCategory> _repository;

    public SearchHotlineCategoriesRequestHandler(IReadRepository<EduDocumentCategory> repository) => _repository = repository;

    public async Task<PaginationResponse<EduDocumentCategoryDto>> Handle(SearchEduDocumentCategoriesRequest request, CancellationToken cancellationToken)
    {
        var spec = new HotlineCategoriesBySearchRequestSpec(request);

        var list = await _repository.ListAsync(spec, cancellationToken);
        int count = await _repository.CountAsync(spec, cancellationToken);

        return new PaginationResponse<EduDocumentCategoryDto>(list, count, request.PageNumber, request.PageSize);
    }
}