namespace TD.CitizenAPI.Application.Catalog.Passengers;

public class CreatePassengerRequest : IRequest<Result<Guid>>
{
    public string Name { get; set; } = default!;
    public string? Email { get; set; }
    public string? Phone { get; set; }
}

public class CreatePassengerRequestValidator : CustomValidator<CreatePassengerRequest>
{
    public CreatePassengerRequestValidator(IReadRepository<Passenger> repository, IStringLocalizer<CreatePassengerRequestValidator> localizer) =>
        RuleFor(p => p.Name).NotEmpty();
}

public class CreateVehicleRequestHandler : IRequestHandler<CreatePassengerRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Passenger> _repository;

    public CreateVehicleRequestHandler(IRepositoryWithEvents<Passenger> repository) => _repository = repository;

    public async Task<Result<Guid>> Handle(CreatePassengerRequest request, CancellationToken cancellationToken)
    {
        var item = new Passenger(request.Name, request.Email, request.Phone);
        await _repository.AddAsync(item, cancellationToken);
        return Result<Guid>.Success(item.Id);
    }
}