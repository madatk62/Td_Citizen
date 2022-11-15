namespace TD.CitizenAPI.Application.Catalog.EduDocumentCatalogues;

public class GetEduDocumentCatalogueRequest : IRequest<Result<EduDocumentCatalogueDetailsDto>>
{
    public Guid Id { get; set; }

    public GetEduDocumentCatalogueRequest(Guid id) => Id = id;
}

public class EduDocumentCatalogueByIdSpec : Specification<EduDocumentCatalogue, EduDocumentCatalogueDetailsDto>, ISingleResultSpecification
{
    public EduDocumentCatalogueByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetEduDocumentCatalogueRequestHandler : IRequestHandler<GetEduDocumentCatalogueRequest, Result<EduDocumentCatalogueDetailsDto>>
{
    private readonly IRepository<EduDocumentCatalogue> _repository;
    private readonly IStringLocalizer<GetEduDocumentCatalogueRequestHandler> _localizer;

    public GetEduDocumentCatalogueRequestHandler(IRepository<EduDocumentCatalogue> repository, IStringLocalizer<GetEduDocumentCatalogueRequestHandler> localizer) => (_repository, _localizer) = (repository, localizer);

    public async Task<Result<EduDocumentCatalogueDetailsDto>> Handle(GetEduDocumentCatalogueRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetBySpecAsync(
            (ISpecification<EduDocumentCatalogue, EduDocumentCatalogueDetailsDto>)new EduDocumentCatalogueByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["EduDocumentCatalogue.notfound"], request.Id));
        return Result<EduDocumentCatalogueDetailsDto>.Success(item);

    }
}