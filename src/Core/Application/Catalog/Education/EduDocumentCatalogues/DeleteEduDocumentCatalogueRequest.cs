namespace TD.CitizenAPI.Application.Catalog.EduDocumentCatalogues;

public class DeleteEduDocumentCatalogueRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }

    public DeleteEduDocumentCatalogueRequest(Guid id) => Id = id;
}

public class DeleteEduDocumentCatalogueRequestHandler : IRequestHandler<DeleteEduDocumentCatalogueRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<EduDocumentCatalogue> _eduDocumentCatalogueRepo;
    private readonly IStringLocalizer<DeleteEduDocumentCatalogueRequestHandler> _localizer;

    public DeleteEduDocumentCatalogueRequestHandler(IRepositoryWithEvents<EduDocumentCatalogue> EduDocumentCatalogueRepo,  IStringLocalizer<DeleteEduDocumentCatalogueRequestHandler> localizer) =>
        (_eduDocumentCatalogueRepo,  _localizer) = (EduDocumentCatalogueRepo,  localizer);

    public async Task<Result<Guid>> Handle(DeleteEduDocumentCatalogueRequest request, CancellationToken cancellationToken)
    {
       
        var item = await _eduDocumentCatalogueRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(_localizer["EduDocumentCatalogue.notfound"]);

        await _eduDocumentCatalogueRepo.DeleteAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}