namespace TD.CitizenAPI.Application.Catalog.Tickets;

public class GetTicketRequest : IRequest<Result<TicketDetailsDto>>
{
    public Guid Id { get; set; }

    public GetTicketRequest(Guid id) => Id = id;
}

public class TicketByIdSpec : Specification<Ticket, TicketDetailsDto>, ISingleResultSpecification
{
    public TicketByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetTicketRequestHandler : IRequestHandler<GetTicketRequest, Result<TicketDetailsDto>>
{
    private readonly IRepository<Ticket> _repository;
    private readonly IStringLocalizer<GetTicketRequestHandler> _localizer;

    public GetTicketRequestHandler(IRepository<Ticket> repository, IStringLocalizer<GetTicketRequestHandler> localizer) => (_repository, _localizer) = (repository, localizer);

    public async Task<Result<TicketDetailsDto>> Handle(GetTicketRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetBySpecAsync(
            (ISpecification<Ticket, TicketDetailsDto>)new TicketByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["Ticket.notfound"], request.Id));
        return Result<TicketDetailsDto>.Success(item);

    }
}