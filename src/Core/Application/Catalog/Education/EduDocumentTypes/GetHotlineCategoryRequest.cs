namespace TD.CitizenAPI.Application.Catalog.EduDocumentTypes;

public class GetEduDocumentTypeRequest : IRequest<Result<EduDocumentTypeDetailsDto>>
{
    public Guid Id { get; set; }

    public GetEduDocumentTypeRequest(Guid id) => Id = id;
}

public class EduDocumentTypeByIdSpec : Specification<EduDocumentType, EduDocumentTypeDetailsDto>, ISingleResultSpecification
{
    public EduDocumentTypeByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetEduDocumentTypeRequestHandler : IRequestHandler<GetEduDocumentTypeRequest, Result<EduDocumentTypeDetailsDto>>
{
    private readonly IRepository<EduDocumentType> _repository;
    private readonly IStringLocalizer<GetEduDocumentTypeRequestHandler> _localizer;

    public GetEduDocumentTypeRequestHandler(IRepository<EduDocumentType> repository, IStringLocalizer<GetEduDocumentTypeRequestHandler> localizer) => (_repository, _localizer) = (repository, localizer);

    public async Task<Result<EduDocumentTypeDetailsDto>> Handle(GetEduDocumentTypeRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetBySpecAsync(
            (ISpecification<EduDocumentType, EduDocumentTypeDetailsDto>)new EduDocumentTypeByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["EduDocumentType.notfound"], request.Id));
        return Result<EduDocumentTypeDetailsDto>.Success(item);

    }
}