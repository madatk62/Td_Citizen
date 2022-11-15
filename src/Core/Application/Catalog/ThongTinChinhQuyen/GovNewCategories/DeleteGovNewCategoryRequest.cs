namespace TD.CitizenAPI.Application.Catalog.GovNewCategories;

public class DeleteGovNewCategoryRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }

    public DeleteGovNewCategoryRequest(Guid id) => Id = id;
}

public class DeleteGovNewCategoryRequestHandler : IRequestHandler<DeleteGovNewCategoryRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<GovNewCategory> _govNewCategoryRepo;
    private readonly IStringLocalizer<DeleteGovNewCategoryRequestHandler> _localizer;

    public DeleteGovNewCategoryRequestHandler(IRepositoryWithEvents<GovNewCategory> GovNewCategoryRepo,  IStringLocalizer<DeleteGovNewCategoryRequestHandler> localizer) =>
        (_govNewCategoryRepo, _localizer) = (GovNewCategoryRepo,  localizer);

    public async Task<Result<Guid>> Handle(DeleteGovNewCategoryRequest request, CancellationToken cancellationToken)
    {
        var item = await _govNewCategoryRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(_localizer["GovNewCategory.notfound"]);

        await _govNewCategoryRepo.DeleteAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}