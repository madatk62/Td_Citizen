namespace TD.CitizenAPI.Application.Catalog.Feedbacks;

public class SearchFeedbacksRequest : PaginationFilter, IRequest<PaginationResponse<FeedbackDto>>
{
    public string? UserName { get; set; }
    public string? Type { get; set; }
    public Guid? DocId { get; set; }
    public int? Status { get; set; }
}

public class FeedbackBySearchRequestSpec : EntitiesByPaginationFilterSpec<Feedback, FeedbackDto>
{
    public FeedbackBySearchRequestSpec(SearchFeedbacksRequest request)
        : base(request) =>
        Query.OrderBy(c => c.CreatedOn, !request.HasOrderBy())
        .Where(p => p.UserName.Equals(request.UserName), request.UserName is not null)
        .Where(p => p.Type.Equals(request.Type), request.Type is not null)
        .Where(p => p.DocId.Equals(request.DocId!.Value), request.DocId.HasValue)
        .Where(p => p.Status.Equals(request.Status!.Value), request.Status.HasValue);
}

public class SearchFeedbacksRequestHandler : IRequestHandler<SearchFeedbacksRequest, PaginationResponse<FeedbackDto>>
{
    private readonly IReadRepository<Feedback> _repository;

    public SearchFeedbacksRequestHandler(IReadRepository<Feedback> repository) => _repository = repository;

    public async Task<PaginationResponse<FeedbackDto>> Handle(SearchFeedbacksRequest request, CancellationToken cancellationToken)
    {
        var spec = new FeedbackBySearchRequestSpec(request);

        var list = await _repository.ListAsync(spec, cancellationToken);
        int count = await _repository.CountAsync(spec, cancellationToken);

        return new PaginationResponse<FeedbackDto>(list, count, request.PageNumber, request.PageSize);
    }
}