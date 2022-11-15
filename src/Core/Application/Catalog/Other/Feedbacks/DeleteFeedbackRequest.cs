namespace TD.CitizenAPI.Application.Catalog.Feedbacks;

public class DeleteFeedbackRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }

    public DeleteFeedbackRequest(Guid id) => Id = id;
}

public class DeleteFeedbackRequestHandler : IRequestHandler<DeleteFeedbackRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Feedback> _repo;
    private readonly IStringLocalizer<DeleteFeedbackRequestHandler> _localizer;

    public DeleteFeedbackRequestHandler(IRepositoryWithEvents<Feedback> FeedbackRepo,  IStringLocalizer<DeleteFeedbackRequestHandler> localizer) =>
        (_repo,  _localizer) = (FeedbackRepo,  localizer);

    public async Task<Result<Guid>> Handle(DeleteFeedbackRequest request, CancellationToken cancellationToken)
    {

        var item = await _repo.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(_localizer["Feedback.notfound"]);

        await _repo.DeleteAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}