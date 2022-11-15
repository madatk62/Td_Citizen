namespace TD.CitizenAPI.Application.Catalog.HoSoDienTus;

public class SearchHoSoDienTusRequest : PaginationFilter, IRequest<PaginationResponse<HoSoDienTuDto>>
{
    public string? IDCongDan { get; set; }
    public string? MaThuTuc { get; set; }
    public string? MaNhomHoSo { get;set; }
    public string? MaLoaiHoSo { get; set; }

}

public class HoSoDienTusBySearchRequestSpec : EntitiesByPaginationFilterSpec<HoSoDienTu, HoSoDienTuDto>
{
    public HoSoDienTusBySearchRequestSpec(SearchHoSoDienTusRequest request)
        : base(request) =>
        Query.Where(c => c.IDCongDan == request.IDCongDan, request.IDCongDan is not null)
            .Where(t => t.MaThuTuc == request.MaThuTuc, request.MaThuTuc is not null)
            .Where(t => t.MaNhomHoSo == request.MaNhomHoSo, request.MaNhomHoSo is not null)
            .Where(t => t.MaLoaiHoSo == request.MaLoaiHoSo, request.MaLoaiHoSo is not null)
            .OrderBy(c => c.TenHoSo, !request.HasOrderBy());
}

public class SearchHoSoDienTusRequestHandler : IRequestHandler<SearchHoSoDienTusRequest, PaginationResponse<HoSoDienTuDto>>
{
    private readonly IReadRepository<HoSoDienTu> _repository;

    public SearchHoSoDienTusRequestHandler(IReadRepository<HoSoDienTu> repository) => _repository = repository;

    public async Task<PaginationResponse<HoSoDienTuDto>> Handle(SearchHoSoDienTusRequest request, CancellationToken cancellationToken)
    {
        var spec = new HoSoDienTusBySearchRequestSpec(request);

        var list = await _repository.ListAsync(spec, cancellationToken);
        int count = await _repository.CountAsync(spec, cancellationToken);

        return new PaginationResponse<HoSoDienTuDto>(list, count, request.PageNumber, request.PageSize);
    }
}