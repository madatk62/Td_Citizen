namespace TD.CitizenAPI.Application.Catalog.HoSoDienTus;

public class GetGiayToHoSoDienTuRequest : IRequest<Result<GiayToHoSoDienTuDetailsDto>>
{
    public Guid Id { get; set; }

    public GetGiayToHoSoDienTuRequest(Guid id) => Id = id;
}

public class GiayToHoSoDienTuByIdSpec : Specification<GiayToHoSoDienTu, GiayToHoSoDienTuDetailsDto>, ISingleResultSpecification
{
    public GiayToHoSoDienTuByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetGiayToHoSoDienTuRequestHandler : IRequestHandler<GetGiayToHoSoDienTuRequest, Result<GiayToHoSoDienTuDetailsDto>>
{
    private readonly IRepository<GiayToHoSoDienTu> _repository;
    private readonly IStringLocalizer<GetGiayToHoSoDienTuRequestHandler> _localizer;

    public GetGiayToHoSoDienTuRequestHandler(IRepository<GiayToHoSoDienTu> repository, IStringLocalizer<GetGiayToHoSoDienTuRequestHandler> localizer) => (_repository, _localizer) = (repository, localizer);

    public async Task<Result<GiayToHoSoDienTuDetailsDto>> Handle(GetGiayToHoSoDienTuRequest request, CancellationToken cancellationToken)
    {
        var tmp = (ISpecification<GiayToHoSoDienTu, GiayToHoSoDienTuDetailsDto>)new GiayToHoSoDienTuByIdSpec(request.Id);
        var item = await _repository.GetBySpecAsync(
            (ISpecification<GiayToHoSoDienTu, GiayToHoSoDienTuDetailsDto>)new GiayToHoSoDienTuByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["hotlinecategory.notfound"], request.Id));
        return Result<GiayToHoSoDienTuDetailsDto>.Success(item);

    }
}