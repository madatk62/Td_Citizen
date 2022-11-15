namespace TD.CitizenAPI.Application.Catalog.EduDocumentCategories;

public class DeleteEduDocumentCategoryRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }

    public DeleteEduDocumentCategoryRequest(Guid id) => Id = id;
}

public class DeleteEduDocumentCategoryRequestHandler : IRequestHandler<DeleteEduDocumentCategoryRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<EduDocumentCategory> _eduDocumentCategoryRepo;
    private readonly IStringLocalizer<DeleteEduDocumentCategoryRequestHandler> _localizer;

    public DeleteEduDocumentCategoryRequestHandler(IRepositoryWithEvents<EduDocumentCategory> EduDocumentCategoryRepo, IStringLocalizer<DeleteEduDocumentCategoryRequestHandler> localizer) =>
        (_eduDocumentCategoryRepo,  _localizer) = (EduDocumentCategoryRepo,  localizer);

    public async Task<Result<Guid>> Handle(DeleteEduDocumentCategoryRequest request, CancellationToken cancellationToken)
    {
        var item = await _eduDocumentCategoryRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(_localizer["EduDocumentCategory.notfound"]);

        await _eduDocumentCategoryRepo.DeleteAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}