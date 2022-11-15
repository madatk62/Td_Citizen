namespace TD.CitizenAPI.Application.Catalog.Tickets;

public class SearchTicketsRequest : PaginationFilter, IRequest<PaginationResponse<TicketDto>>
{
}

public class TicketsBySearchRequestSpec : EntitiesByPaginationFilterSpec<Ticket, TicketDto>
{
    public TicketsBySearchRequestSpec(SearchTicketsRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Date, !request.HasOrderBy());
}

public class SearchTicketsRequestHandler : IRequestHandler<SearchTicketsRequest, PaginationResponse<TicketDto>>
{
    private readonly IReadRepository<Ticket> _repository;

    public SearchTicketsRequestHandler(IReadRepository<Ticket> repository) => _repository = repository;

    public async Task<PaginationResponse<TicketDto>> Handle(SearchTicketsRequest request, CancellationToken cancellationToken)
    {
        var spec = new TicketsBySearchRequestSpec(request);

        var list = await _repository.ListAsync(spec, cancellationToken);
        int count = await _repository.CountAsync(spec, cancellationToken);

        return new PaginationResponse<TicketDto>(list, count, request.PageNumber, request.PageSize);
    }
}