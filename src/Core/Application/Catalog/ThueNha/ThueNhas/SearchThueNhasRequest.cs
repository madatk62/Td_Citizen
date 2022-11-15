namespace TD.CitizenAPI.Application.Catalog.ThueNhas;

public class SearchThueNhasRequest : PaginationFilter, IRequest<PaginationResponse<ThueNhaDto>>
{
    public Guid? ProvinceId { get; set; }
    public Guid? DistrictId { get; set; }
    public Guid? CommuneId { get; set; }
    public string? UserName { get; set; }
    public int? Status { get; set; }
}

public class ThueNhasBySearchRequestSpec : EntitiesByPaginationFilterSpec<ThueNha, ThueNhaDto>
{
    public ThueNhasBySearchRequestSpec(SearchThueNhasRequest request)
        : base(request) =>

        Query.Include(p => p.Commune)
            .Include(p => p.Province)
            .Include(p => p.District)
        .Where(p => p.CommuneId.Equals(request.CommuneId!.Value), request.CommuneId.HasValue)
        .Where(p => p.ProvinceId.Equals(request.ProvinceId!.Value), request.ProvinceId.HasValue)
        .Where(p => p.DistrictId.Equals(request.DistrictId!.Value), request.DistrictId.HasValue)
        .Where(p => p.Status.Equals(request.Status!.Value), request.Status.HasValue)
        .Where(p => p.UserName == request.UserName, !string.IsNullOrEmpty(request.UserName))
        .OrderBy(c => c.Name, !request.HasOrderBy());
}

public class SearchThueNhasRequestHandler : IRequestHandler<SearchThueNhasRequest, PaginationResponse<ThueNhaDto>>
{
    private readonly IReadRepository<ThueNha> _repository;

    public SearchThueNhasRequestHandler(IReadRepository<ThueNha> repository) => _repository = repository;

    public async Task<PaginationResponse<ThueNhaDto>> Handle(SearchThueNhasRequest request, CancellationToken cancellationToken)
    {
        var spec = new ThueNhasBySearchRequestSpec(request);

        var list = await _repository.ListAsync(spec, cancellationToken);
        int count = await _repository.CountAsync(spec, cancellationToken);

        return new PaginationResponse<ThueNhaDto>(list, count, request.PageNumber, request.PageSize);
    }
}