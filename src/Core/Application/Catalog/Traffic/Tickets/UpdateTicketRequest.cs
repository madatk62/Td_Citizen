namespace TD.CitizenAPI.Application.Catalog.Tickets;

public class UpdateTicketRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public Guid? TripId { get; set; }
    public Guid? PassengerId { get; set; }
    public string? Seat { get; set; }
    public DateTime? Date { get; set; }
    public virtual Trip? Trip { get; set; }
    public virtual Passenger? Passenger { get; set; }
}

public class UpdateTicketRequestValidator : CustomValidator<UpdateTicketRequest>
{
    public UpdateTicketRequestValidator(IRepository<Ticket> repository, IStringLocalizer<UpdateTicketRequestValidator> localizer) =>
        RuleFor(p => p.PassengerId)
            .NotEmpty();
}

public class UpdateTicketRequestHandler : IRequestHandler<UpdateTicketRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Ticket> _repository;
    private readonly IStringLocalizer<UpdateTicketRequestHandler> _localizer;

    public UpdateTicketRequestHandler(IRepositoryWithEvents<Ticket> repository, IStringLocalizer<UpdateTicketRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Result<Guid>> Handle(UpdateTicketRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(string.Format(_localizer["marketcategory.notfound"], request.Id));

        item.Update(request.TripId, request.PassengerId, request.Seat, request.Date);

        await _repository.UpdateAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}