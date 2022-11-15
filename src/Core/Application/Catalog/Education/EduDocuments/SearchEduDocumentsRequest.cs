namespace TD.CitizenAPI.Application.Catalog.EduDocuments;

public class SearchEduDocumentsRequest : PaginationFilter, IRequest<PaginationResponse<EduDocumentDto>>
{
    public Guid? EduDocumentCategoryId { get; set; }
    public Guid? EduDocumentTypeId { get; set; }

}

public class SearchCategoriesRequestHandler : IRequestHandler<SearchEduDocumentsRequest, PaginationResponse<EduDocumentDto>>
{
    private readonly IReadRepository<EduDocument> _repository;

    public SearchCategoriesRequestHandler(IReadRepository<EduDocument> repository) => _repository = repository;

    public async Task<PaginationResponse<EduDocumentDto>> Handle(SearchEduDocumentsRequest request, CancellationToken cancellationToken)
    {
        var spec = new EduDocumentsBySearchRequestSpec(request);

        var list = await _repository.ListAsync(spec, cancellationToken);
        int count = await _repository.CountAsync(spec, cancellationToken);

        return new PaginationResponse<EduDocumentDto>(list, count, request.PageNumber, request.PageSize);
    }
}