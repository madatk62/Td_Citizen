namespace TD.CitizenAPI.Application.Catalog.EduDocumentTypes;

public class SearchEduDocumentTypesRequest : PaginationFilter, IRequest<PaginationResponse<EduDocumentTypeDto>>
{
}

public class HotlineCategoriesBySearchRequestSpec : EntitiesByPaginationFilterSpec<EduDocumentType, EduDocumentTypeDto>
{
    public HotlineCategoriesBySearchRequestSpec(SearchEduDocumentTypesRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class SearchHotlineCategoriesRequestHandler : IRequestHandler<SearchEduDocumentTypesRequest, PaginationResponse<EduDocumentTypeDto>>
{
    private readonly IReadRepository<EduDocumentType> _repository;

    public SearchHotlineCategoriesRequestHandler(IReadRepository<EduDocumentType> repository) => _repository = repository;

    public async Task<PaginationResponse<EduDocumentTypeDto>> Handle(SearchEduDocumentTypesRequest request, CancellationToken cancellationToken)
    {
        var spec = new HotlineCategoriesBySearchRequestSpec(request);

        var list = await _repository.ListAsync(spec, cancellationToken);
        int count = await _repository.CountAsync(spec, cancellationToken);

        return new PaginationResponse<EduDocumentTypeDto>(list, count, request.PageNumber, request.PageSize);
    }
}