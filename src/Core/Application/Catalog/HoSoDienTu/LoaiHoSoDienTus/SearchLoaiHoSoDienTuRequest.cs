namespace TD.CitizenAPI.Application.Catalog.LoaiHoSoDienTus;

public class SearchLoaiHoSoDienTusRequest : PaginationFilter, IRequest<PaginationResponse<LoaiHoSoDienTuDto>>
{
    public string? IDCongDan { get; set; }
}

public class LoaiHoSoDienTusBySearchRequestSpec : EntitiesByPaginationFilterSpec<LoaiHoSoDienTu, LoaiHoSoDienTuDto>
{
    public LoaiHoSoDienTusBySearchRequestSpec(SearchLoaiHoSoDienTusRequest request)
        : base(request) =>
        Query.Where(t => t.IDCongDan == request.IDCongDan, request.IDCongDan is not null)
        .OrderBy(c => c.Ten, !request.HasOrderBy());
}

public class SearchLoaiHoSoDienTusRequestHandler : IRequestHandler<SearchLoaiHoSoDienTusRequest, PaginationResponse<LoaiHoSoDienTuDto>>
{
    private readonly IReadRepository<LoaiHoSoDienTu> _repository;

    public SearchLoaiHoSoDienTusRequestHandler(IReadRepository<LoaiHoSoDienTu> repository) => _repository = repository;

    public async Task<PaginationResponse<LoaiHoSoDienTuDto>> Handle(SearchLoaiHoSoDienTusRequest request, CancellationToken cancellationToken)
    {
        var spec = new LoaiHoSoDienTusBySearchRequestSpec(request);

        var list = await _repository.ListAsync(spec, cancellationToken);
        int count = await _repository.CountAsync(spec, cancellationToken);

        return new PaginationResponse<LoaiHoSoDienTuDto>(list, count, request.PageNumber, request.PageSize);
    }
}