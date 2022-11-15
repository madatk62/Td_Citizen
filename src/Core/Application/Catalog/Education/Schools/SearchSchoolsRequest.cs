namespace TD.CitizenAPI.Application.Catalog.Schools;

public class SearchSchoolsRequest : PaginationFilter, IRequest<PaginationResponse<SchoolDto>>
{
    public Guid? ProvinceId { get; set; }
    public Guid? DistrictId { get; set; }
    public Guid? CommuneId { get; set; }
}

public class SchoolsBySearchRequestSpec : EntitiesByPaginationFilterSpec<School, SchoolDto>
{
    public SchoolsBySearchRequestSpec(SearchSchoolsRequest request)
        : base(request) =>
        Query.Include(p => p.Commune)
        .Include(p => p.District)
        .Include(p => p.Province)
        .Where(p => p.CommuneId.Equals(request.CommuneId!.Value), request.CommuneId.HasValue)
        .Where(p => p.ProvinceId.Equals(request.ProvinceId!.Value), request.ProvinceId.HasValue)
        .Where(p => p.DistrictId.Equals(request.DistrictId!.Value), request.DistrictId.HasValue).OrderBy(c => c.Name, !request.HasOrderBy());
}

public class SearchSchoolsRequestHandler : IRequestHandler<SearchSchoolsRequest, PaginationResponse<SchoolDto>>
{
    private readonly IReadRepository<School> _repository;

    public SearchSchoolsRequestHandler(IReadRepository<School> repository) => _repository = repository;

    public async Task<PaginationResponse<SchoolDto>> Handle(SearchSchoolsRequest request, CancellationToken cancellationToken)
    {
        var spec = new SchoolsBySearchRequestSpec(request);

        var list = await _repository.ListAsync(spec, cancellationToken);
        int count = await _repository.CountAsync(spec, cancellationToken);

        return new PaginationResponse<SchoolDto>(list, count, request.PageNumber, request.PageSize);
    }
}