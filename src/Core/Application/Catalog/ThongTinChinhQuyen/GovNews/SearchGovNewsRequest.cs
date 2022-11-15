namespace TD.CitizenAPI.Application.Catalog.GovNews;

public class SearchGovNewsRequest : PaginationFilter, IRequest<PaginationResponse<GovNewDto>>
{
    public Guid? GovNewCategoryId { get; set; }
    public Guid? ProvinceId { get; set; }
    public Guid? DistrictId { get; set; }
    public Guid? CommuneId { get; set; }
    public bool? IsStar { get; set; }
    public bool? IsPublic { get; set; }
    public bool? IsNotification { get; set; }

}

public class SearchCategoriesRequestHandler : IRequestHandler<SearchGovNewsRequest, PaginationResponse<GovNewDto>>
{
    private readonly IReadRepository<GovNew> _repository;

    public SearchCategoriesRequestHandler(IReadRepository<GovNew> repository) => _repository = repository;

    public async Task<PaginationResponse<GovNewDto>> Handle(SearchGovNewsRequest request, CancellationToken cancellationToken)
    {
        var spec = new GovNewsBySearchRequestSpec(request);

        var list = await _repository.ListAsync(spec, cancellationToken);
        int count = await _repository.CountAsync(spec, cancellationToken);

        return new PaginationResponse<GovNewDto>(list, count, request.PageNumber, request.PageSize);
    }
}