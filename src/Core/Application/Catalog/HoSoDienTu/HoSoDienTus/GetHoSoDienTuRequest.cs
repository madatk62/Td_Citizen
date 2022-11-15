namespace TD.CitizenAPI.Application.Catalog.HoSoDienTus;

public class GetHoSoDienTuRequest : IRequest<Result<HoSoDienTuDetailsDto>>
{
    public Guid Id { get; set; }

    public GetHoSoDienTuRequest(Guid id) => Id = id;
}

public class HoSoDienTuByIdSpec : Specification<HoSoDienTu, HoSoDienTuDetailsDto>, ISingleResultSpecification
{
    public HoSoDienTuByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetHoSoDienTuRequestHandler : IRequestHandler<GetHoSoDienTuRequest, Result<HoSoDienTuDetailsDto>>
{
    private readonly IRepository<HoSoDienTu> _repository;
    private readonly IStringLocalizer<GetHoSoDienTuRequestHandler> _localizer;

    public GetHoSoDienTuRequestHandler(IRepository<HoSoDienTu> repository, IStringLocalizer<GetHoSoDienTuRequestHandler> localizer) => (_repository, _localizer) = (repository, localizer);

    public async Task<Result<HoSoDienTuDetailsDto>> Handle(GetHoSoDienTuRequest request, CancellationToken cancellationToken)
    {
        var tmp = (ISpecification<HoSoDienTu, HoSoDienTuDetailsDto>) new HoSoDienTuByIdSpec(request.Id);
        var item = await _repository.GetBySpecAsync(
            (ISpecification<HoSoDienTu, HoSoDienTuDetailsDto>)new HoSoDienTuByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["hotlinecategory.notfound"], request.Id));
        return Result<HoSoDienTuDetailsDto>.Success(item);

    }
}