namespace TD.CitizenAPI.Application.Catalog.Passengers;

public class SearchPassengersRequest : PaginationFilter, IRequest<PaginationResponse<PassengerDto>>
{
}

public class PassengersBySearchRequestSpec : EntitiesByPaginationFilterSpec<Passenger, PassengerDto>
{
    public PassengersBySearchRequestSpec(SearchPassengersRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class SearchPassengersRequestHandler : IRequestHandler<SearchPassengersRequest, PaginationResponse<PassengerDto>>
{
    private readonly IReadRepository<Passenger> _repository;

    public SearchPassengersRequestHandler(IReadRepository<Passenger> repository) => _repository = repository;

    public async Task<PaginationResponse<PassengerDto>> Handle(SearchPassengersRequest request, CancellationToken cancellationToken)
    {
        var spec = new PassengersBySearchRequestSpec(request);

        var list = await _repository.ListAsync(spec, cancellationToken);
        int count = await _repository.CountAsync(spec, cancellationToken);

        return new PaginationResponse<PassengerDto>(list, count, request.PageNumber, request.PageSize);
    }
}