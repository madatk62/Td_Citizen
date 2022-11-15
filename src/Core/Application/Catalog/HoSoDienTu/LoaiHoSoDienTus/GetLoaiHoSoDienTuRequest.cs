namespace TD.CitizenAPI.Application.Catalog.LoaiHoSoDienTus;

public class GetLoaiHoSoDienTuRequest : IRequest<Result<LoaiHoSoDienTuDetailDto>>
{
    public Guid Id { get; set; }

    public GetLoaiHoSoDienTuRequest(Guid id) => Id = id;
}

public class LoaiHoSoDienTuByIdSpec : Specification<LoaiHoSoDienTu, LoaiHoSoDienTuDetailDto>, ISingleResultSpecification
{
    public LoaiHoSoDienTuByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetLoaiHoSoDienTuRequestHandler : IRequestHandler<GetLoaiHoSoDienTuRequest, Result<LoaiHoSoDienTuDetailDto>>
{
    private readonly IRepository<LoaiHoSoDienTu> _repository;
    private readonly IStringLocalizer<GetLoaiHoSoDienTuRequestHandler> _localizer;

    public GetLoaiHoSoDienTuRequestHandler(IRepository<LoaiHoSoDienTu> repository, IStringLocalizer<GetLoaiHoSoDienTuRequestHandler> localizer) => (_repository, _localizer) = (repository, localizer);

    public async Task<Result<LoaiHoSoDienTuDetailDto>> Handle(GetLoaiHoSoDienTuRequest request, CancellationToken cancellationToken)
    {
        var tmp = (ISpecification<LoaiHoSoDienTu, LoaiHoSoDienTuDetailDto>)new LoaiHoSoDienTuByIdSpec(request.Id);
        var item = await _repository.GetBySpecAsync(
            (ISpecification<LoaiHoSoDienTu, LoaiHoSoDienTuDetailDto>)new LoaiHoSoDienTuByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["LoaiHoSoDienTu.notfound"], request.Id));
        return Result<LoaiHoSoDienTuDetailDto>.Success(item);

    }
}