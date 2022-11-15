namespace TD.CitizenAPI.Application.Catalog.OrganizationUnitUsers;

public class SearchOrganizationUnitUsersRequest : PaginationFilter, IRequest<PaginationResponse<OrganizationUnitUserDto>>
{
    public Guid? OrganizationUnitId { get; set; }
}

public class MarketCategoriesBySearchRequestSpec : EntitiesByPaginationFilterSpec<OrganizationUnitUser, OrganizationUnitUserDto>
{
    public MarketCategoriesBySearchRequestSpec(SearchOrganizationUnitUsersRequest request)
        : base(request) =>
        Query.Include(x => x.OrganizationUnit)
       .Where(p => p.OrganizationUnitId.Equals(request.OrganizationUnitId!.Value), request.OrganizationUnitId.HasValue);
}

public class SearchMarketCategoriesRequestHandler : IRequestHandler<SearchOrganizationUnitUsersRequest, PaginationResponse<OrganizationUnitUserDto>>
{
    private readonly IReadRepository<OrganizationUnitUser> _repository;

    public SearchMarketCategoriesRequestHandler(IReadRepository<OrganizationUnitUser> repository) => _repository = repository;

    public async Task<PaginationResponse<OrganizationUnitUserDto>> Handle(SearchOrganizationUnitUsersRequest request, CancellationToken cancellationToken)
    {
        var spec = new MarketCategoriesBySearchRequestSpec(request);

        var list = await _repository.ListAsync(spec, cancellationToken);
        int count = await _repository.CountAsync(spec, cancellationToken);

        return new PaginationResponse<OrganizationUnitUserDto>(list, count, request.PageNumber, request.PageSize);
    }
}