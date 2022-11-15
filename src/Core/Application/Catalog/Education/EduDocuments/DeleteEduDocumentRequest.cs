namespace TD.CitizenAPI.Application.Catalog.EduDocuments;

public class DeleteEduDocumentRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }

    public DeleteEduDocumentRequest(Guid id) => Id = id;
}

public class DeleteEduDocumentRequestHandler : IRequestHandler<DeleteEduDocumentRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<EduDocument> _repository;
    private readonly IStringLocalizer<DeleteEduDocumentRequestHandler> _localizer;

    public DeleteEduDocumentRequestHandler(IRepositoryWithEvents<EduDocument> repository, IStringLocalizer<DeleteEduDocumentRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Result<Guid>> Handle(DeleteEduDocumentRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(_localizer["EduDocument.notfound"]);

        await _repository.DeleteAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}