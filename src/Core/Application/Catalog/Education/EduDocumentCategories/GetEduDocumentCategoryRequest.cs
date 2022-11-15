namespace TD.CitizenAPI.Application.Catalog.EduDocumentCategories;

public class GetEduDocumentCategoryRequest : IRequest<Result<EduDocumentCategoryDetailsDto>>
{
    public Guid Id { get; set; }

    public GetEduDocumentCategoryRequest(Guid id) => Id = id;
}

public class EduDocumentCategoryByIdSpec : Specification<EduDocumentCategory, EduDocumentCategoryDetailsDto>, ISingleResultSpecification
{
    public EduDocumentCategoryByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetEduDocumentCategoryRequestHandler : IRequestHandler<GetEduDocumentCategoryRequest, Result<EduDocumentCategoryDetailsDto>>
{
    private readonly IRepository<EduDocumentCategory> _repository;
    private readonly IStringLocalizer<GetEduDocumentCategoryRequestHandler> _localizer;

    public GetEduDocumentCategoryRequestHandler(IRepository<EduDocumentCategory> repository, IStringLocalizer<GetEduDocumentCategoryRequestHandler> localizer) => (_repository, _localizer) = (repository, localizer);

    public async Task<Result<EduDocumentCategoryDetailsDto>> Handle(GetEduDocumentCategoryRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetBySpecAsync(
            (ISpecification<EduDocumentCategory, EduDocumentCategoryDetailsDto>)new EduDocumentCategoryByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["EduDocumentCategory.notfound"], request.Id));
        return Result<EduDocumentCategoryDetailsDto>.Success(item);

    }
}