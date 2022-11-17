namespace TD.CitizenAPI.Application.Catalog.HoSoDienTus;

public class SearchGiayToHoSoDienTusRequest : PaginationFilter, IRequest<PaginationResponse<GiayToHoSoDienTuDto>>
{
    public string? IDCongDan { get; set; }
    public string? HoSoDienTuID { get; set; }
    public string? LoaiGiayToID { get; set; }
    public string? NhomGiayToID { get; set; }
}

public class GiayToHoSoDienTusBySearchRequestSpec : EntitiesByPaginationFilterSpec<GiayToHoSoDienTu, GiayToHoSoDienTuDto>
{
    public GiayToHoSoDienTusBySearchRequestSpec(SearchGiayToHoSoDienTusRequest request)
        : base(request)
    {
        Query.Where(t => t.IDCongDan == request.IDCongDan, request.IDCongDan is not null)
        .Where(t => t.HoSoDienTuID == request.HoSoDienTuID, request.HoSoDienTuID is not null)
        .Where(t => t.LoaiGiayToID == request.LoaiGiayToID, request.LoaiGiayToID is not null)
        .Where(t => t.NhomGiayToID == request.NhomGiayToID, request.NhomGiayToID is not null)
        .Where(t => t.CreatedOn >= request.TuNgay && t.CreatedOn.HasValue,  request.TuNgay is not null)
        .Where(t => t.CreatedOn < request.DenNgay && t.CreatedOn.HasValue, request.DenNgay is not null)
        .OrderByDescending(c => c.CreatedOn, !request.HasOrderBy());
    }
}

public class SearchGiayToHoSoDienTusRequestHandler : IRequestHandler<SearchGiayToHoSoDienTusRequest, PaginationResponse<GiayToHoSoDienTuDto>>
{
    private readonly IReadRepository<GiayToHoSoDienTu> _repository;

    public SearchGiayToHoSoDienTusRequestHandler(IReadRepository<GiayToHoSoDienTu> repository) => _repository = repository;

    public async Task<PaginationResponse<GiayToHoSoDienTuDto>> Handle(SearchGiayToHoSoDienTusRequest request, CancellationToken cancellationToken)
    {
        var spec = new GiayToHoSoDienTusBySearchRequestSpec(request);

        var list = await _repository.ListAsync(spec, cancellationToken);
        int count = await _repository.CountAsync(spec, cancellationToken);

        return new PaginationResponse<GiayToHoSoDienTuDto>(list, count, request.PageNumber, request.PageSize);
    }
}