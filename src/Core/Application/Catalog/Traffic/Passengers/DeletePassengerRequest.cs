using TD.CitizenAPI.Application.Catalog.Carpools;

namespace TD.CitizenAPI.Application.Catalog.Passengers;

public class DeletePassengerRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }

    public DeletePassengerRequest(Guid id) => Id = id;
}

public class DeletePassengerRequestHandler : IRequestHandler<DeletePassengerRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Passenger> _repository;
    private readonly IReadRepository<Carpool> _carpoolRepo;
    private readonly IStringLocalizer<DeletePassengerRequestHandler> _localizer;

    public DeletePassengerRequestHandler(IRepositoryWithEvents<Passenger> repository, IReadRepository<Carpool> carpoolRepo, IStringLocalizer<DeletePassengerRequestHandler> localizer) =>
        (_repository, _carpoolRepo, _localizer) = (repository, carpoolRepo, localizer);

    public async Task<Result<Guid>> Handle(DeletePassengerRequest request, CancellationToken cancellationToken)
    {


        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(_localizer["Passenger.notfound"]);

        await _repository.DeleteAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}