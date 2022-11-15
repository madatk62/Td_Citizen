namespace TD.CitizenAPI.Application.Catalog.Tickets;

public class CreateTicketRequest : IRequest<Result<Guid>>
{
    public Guid? TripId { get; set; }
    public Guid? PassengerId { get; set; }
    public string? Seat { get; set; }
    public DateTime? Date { get; set; }
    public virtual Trip? Trip { get; set; }
    public virtual Passenger? Passenger { get; set; }
}

public class CreateTicketRequestValidator : CustomValidator<CreateTicketRequest>
{
    public CreateTicketRequestValidator(IReadRepository<Ticket> repository, IStringLocalizer<CreateTicketRequestValidator> localizer) =>
        RuleFor(p => p.PassengerId).NotEmpty();
}

public class CreateVehicleRequestHandler : IRequestHandler<CreateTicketRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Ticket> _repository;

    public CreateVehicleRequestHandler(IRepositoryWithEvents<Ticket> repository) => _repository = repository;

    public async Task<Result<Guid>> Handle(CreateTicketRequest request, CancellationToken cancellationToken)
    {
        var item = new Ticket(request.TripId, request.PassengerId, request.Seat, request.Date);
        await _repository.AddAsync(item, cancellationToken);
        return Result<Guid>.Success(item.Id);
    }
}