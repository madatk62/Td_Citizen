namespace TD.CitizenAPI.Application.Catalog.GovNewCategories;

public class GetGovNewCategoryRequest : IRequest<Result<GovNewCategoryDetailsDto>>
{
    public Guid Id { get; set; }

    public GetGovNewCategoryRequest(Guid id) => Id = id;
}

public class GovNewCategoryByIdSpec : Specification<GovNewCategory, GovNewCategoryDetailsDto>, ISingleResultSpecification
{
    public GovNewCategoryByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetGovNewCategoryRequestHandler : IRequestHandler<GetGovNewCategoryRequest, Result<GovNewCategoryDetailsDto>>
{
    private readonly IRepository<GovNewCategory> _repository;
    private readonly IStringLocalizer<GetGovNewCategoryRequestHandler> _localizer;

    public GetGovNewCategoryRequestHandler(IRepository<GovNewCategory> repository, IStringLocalizer<GetGovNewCategoryRequestHandler> localizer) => (_repository, _localizer) = (repository, localizer);

    public async Task<Result<GovNewCategoryDetailsDto>> Handle(GetGovNewCategoryRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetBySpecAsync(
            (ISpecification<GovNewCategory, GovNewCategoryDetailsDto>)new GovNewCategoryByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["GovNewCategory.notfound"], request.Id));
        return Result<GovNewCategoryDetailsDto>.Success(item);

    }
}