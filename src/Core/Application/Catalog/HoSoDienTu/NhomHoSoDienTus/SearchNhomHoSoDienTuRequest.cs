namespace TD.CitizenAPI.Application.Catalog.NhomHoSoDienTus;

public class SearchNhomHoSoDienTusRequest : PaginationFilter, IRequest<PaginationResponse<NhomHoSoDienTuDto>>
{
    public string? IDCongDan { get; set; }
}

public class NhomHoSoDienTusBySearchRequestSpec : EntitiesByPaginationFilterSpec<NhomHoSoDienTu, NhomHoSoDienTuDto>
{
    public NhomHoSoDienTusBySearchRequestSpec(SearchNhomHoSoDienTusRequest request)
        : base(request) =>
        Query.Where(t=> t.IDCongDan == request.IDCongDan , request.IDCongDan is not null)
        .OrderBy(c => c.Ten, !request.HasOrderBy());
}

public class SearchNhomHoSoDienTusRequestHandler : IRequestHandler<SearchNhomHoSoDienTusRequest, PaginationResponse<NhomHoSoDienTuDto>>
{
    private readonly IReadRepository<NhomHoSoDienTu> _repository;

    public SearchNhomHoSoDienTusRequestHandler(IReadRepository<NhomHoSoDienTu> repository) => _repository = repository;

    public async Task<PaginationResponse<NhomHoSoDienTuDto>> Handle(SearchNhomHoSoDienTusRequest request, CancellationToken cancellationToken)
    {
        var spec = new NhomHoSoDienTusBySearchRequestSpec(request);

        var list = await _repository.ListAsync(spec, cancellationToken);
        int count = await _repository.CountAsync(spec, cancellationToken);

        return new PaginationResponse<NhomHoSoDienTuDto>(list, count, request.PageNumber, request.PageSize);
    }
}