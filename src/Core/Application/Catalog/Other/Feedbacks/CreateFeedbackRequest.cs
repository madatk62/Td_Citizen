namespace TD.CitizenAPI.Application.Catalog.Feedbacks;

public partial class CreateFeedbackRequest : IRequest<Result<Guid>>
{
    public int Rate { get; set; }
    public string? Description { get; set; }
    public string? Content { get; set; }
    public string? Type { get; set; }
    public Guid? DocId { get; set; }

    public class CreateFeedbackRequestValidator : CustomValidator<CreateFeedbackRequest>
    {
        public CreateFeedbackRequestValidator(IReadRepository<Feedback> repository, IStringLocalizer<CreateFeedbackRequestValidator> localizer)
        {
            }
    }

    public class CreateFeedbackRequestHandler : IRequestHandler<CreateFeedbackRequest, Result<Guid>>
    {
        // Add Domain Events automatically by using IRepositoryWithEvents
        private readonly IRepositoryWithEvents<Feedback> _repository;
        private readonly ICurrentUser _currentUser;

        public CreateFeedbackRequestHandler(IRepositoryWithEvents<Feedback> repository, ICurrentUser currentUser) => (_repository, _currentUser) = (repository, currentUser);

        public async Task<Result<Guid>> Handle(CreateFeedbackRequest request, CancellationToken cancellationToken)
        {
            var item = new Feedback(_currentUser.GetUserName() ?? "", request.Rate, request.Description, request.Content, 0, request.Type, request.DocId);
            await _repository.AddAsync(item, cancellationToken);
            return Result<Guid>.Success(item.Id);
        }
    }
}