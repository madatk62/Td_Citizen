namespace TD.CitizenAPI.Application.Catalog.Passengers;

public class GetPassengerRequest : IRequest<Result<PassengerDetailsDto>>
{
    public Guid Id { get; set; }

    public GetPassengerRequest(Guid id) => Id = id;
}

public class PassengerByIdSpec : Specification<Passenger, PassengerDetailsDto>, ISingleResultSpecification
{
    public PassengerByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetPassengerRequestHandler : IRequestHandler<GetPassengerRequest, Result<PassengerDetailsDto>>
{
    private readonly IRepository<Passenger> _repository;
    private readonly IStringLocalizer<GetPassengerRequestHandler> _localizer;

    public GetPassengerRequestHandler(IRepository<Passenger> repository, IStringLocalizer<GetPassengerRequestHandler> localizer) => (_repository, _localizer) = (repository, localizer);

    public async Task<Result<PassengerDetailsDto>> Handle(GetPassengerRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetBySpecAsync(
            (ISpecification<Passenger, PassengerDetailsDto>)new PassengerByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["Passenger.notfound"], request.Id));
        return Result<PassengerDetailsDto>.Success(item);

    }
}