namespace TD.CitizenAPI.Application.Catalog.NhomHoSoDienTus;

public class GetNhomHoSoDienTuRequest : IRequest<Result<NhomHoSoDienTuDetailDto>>
{
    public Guid Id { get; set; }

    public GetNhomHoSoDienTuRequest(Guid id) => Id = id;
}

public class NhomHoSoDienTuByIdSpec : Specification<NhomHoSoDienTu, NhomHoSoDienTuDetailDto>, ISingleResultSpecification
{
    public NhomHoSoDienTuByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetNhomHoSoDienTuRequestHandler : IRequestHandler<GetNhomHoSoDienTuRequest, Result<NhomHoSoDienTuDetailDto>>
{
    private readonly IRepository<NhomHoSoDienTu> _repository;
    private readonly IStringLocalizer<GetNhomHoSoDienTuRequestHandler> _localizer;

    public GetNhomHoSoDienTuRequestHandler(IRepository<NhomHoSoDienTu> repository, IStringLocalizer<GetNhomHoSoDienTuRequestHandler> localizer) => (_repository, _localizer) = (repository, localizer);

    public async Task<Result<NhomHoSoDienTuDetailDto>> Handle(GetNhomHoSoDienTuRequest request, CancellationToken cancellationToken)
    {
        var tmp = (ISpecification<NhomHoSoDienTu, NhomHoSoDienTuDetailDto>)new NhomHoSoDienTuByIdSpec(request.Id);
        var item = await _repository.GetBySpecAsync(
            (ISpecification<NhomHoSoDienTu, NhomHoSoDienTuDetailDto>)new NhomHoSoDienTuByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["NhomHoSoDienTu.notfound"], request.Id));
        return Result<NhomHoSoDienTuDetailDto>.Success(item);

    }
}