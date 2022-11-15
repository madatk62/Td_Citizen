namespace TD.CitizenAPI.Application.Catalog.GovNews;

public class DeleteGovNewRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }

    public DeleteGovNewRequest(Guid id) => Id = id;
}

public class DeleteGovNewRequestHandler : IRequestHandler<DeleteGovNewRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<GovNew> _repository;
    private readonly IStringLocalizer<DeleteGovNewRequestHandler> _localizer;

    public DeleteGovNewRequestHandler(IRepositoryWithEvents<GovNew> repository, IStringLocalizer<DeleteGovNewRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Result<Guid>> Handle(DeleteGovNewRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(_localizer["GovNew.notfound"]);

        await _repository.DeleteAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}