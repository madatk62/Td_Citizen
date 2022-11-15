namespace TD.CitizenAPI.Application.Catalog.OrganizationUnits;

public class SearchOrganizationUnitsRequest : PaginationFilter, IRequest<PaginationResponse<OrganizationUnitDto>>
{
    public Guid? ParentId { get; set; }
    public string? ParentCode { get; set; }

}

public class OrganizationUnitsBySearchRequestSpec : EntitiesByPaginationFilterSpec<OrganizationUnit, OrganizationUnitDto>
{
    public OrganizationUnitsBySearchRequestSpec(SearchOrganizationUnitsRequest request)
        : base(request) =>
        Query.Include(x => x.Area)
        .Include(x => x.Parent)
        .Where(p => p.ParentId.Equals(request.ParentId!.Value), request.ParentId.HasValue)
        .Where(p => p.ParentCode == request.ParentCode, !string.IsNullOrEmpty(request.ParentCode));
}

public class SearchOrganizationUnitsRequestHandler : IRequestHandler<SearchOrganizationUnitsRequest, PaginationResponse<OrganizationUnitDto>>
{
    private readonly IReadRepository<OrganizationUnit> _repository;

    public SearchOrganizationUnitsRequestHandler(IReadRepository<OrganizationUnit> repository) => _repository = repository;

    public async Task<PaginationResponse<OrganizationUnitDto>> Handle(SearchOrganizationUnitsRequest request, CancellationToken cancellationToken)
    {
        var spec = new OrganizationUnitsBySearchRequestSpec(request);

        var list = await _repository.ListAsync(spec, cancellationToken);
        int count = await _repository.CountAsync(spec, cancellationToken);

        return new PaginationResponse<OrganizationUnitDto>(list, count, request.PageNumber, request.PageSize);
    }
}