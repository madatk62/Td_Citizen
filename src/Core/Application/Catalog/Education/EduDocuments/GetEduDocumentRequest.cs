namespace TD.CitizenAPI.Application.Catalog.EduDocuments;

public class GetEduDocumentRequest : IRequest<Result<EduDocumentDetailsDto>>
{
    public Guid Id { get; set; }

    public GetEduDocumentRequest(Guid id) => Id = id;
}

public class GetEduDocumentRequestHandler : IRequestHandler<GetEduDocumentRequest, Result<EduDocumentDetailsDto>>
{
    private readonly IRepository<EduDocument> _repository;
    private readonly IStringLocalizer<GetEduDocumentRequestHandler> _localizer;

    public GetEduDocumentRequestHandler(IRepository<EduDocument> repository, IStringLocalizer<GetEduDocumentRequestHandler> localizer) => (_repository, _localizer) = (repository, localizer);

    public async Task<Result<EduDocumentDetailsDto>> Handle(GetEduDocumentRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetBySpecAsync(
            (ISpecification<EduDocument, EduDocumentDetailsDto>)new EduDocumentByIdWithIncludeSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["EduDocument.notfound"], request.Id));

        item.EduDocumentCatalogue = item?.EduDocumentCategory?.EduDocumentCatalogue ?? null;
        item.EduDocumentCatalogueId = item?.EduDocumentCategory?.EduDocumentCatalogueId ?? null;

        return Result<EduDocumentDetailsDto>.Success(item);

    }
}