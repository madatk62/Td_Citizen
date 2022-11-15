namespace TD.CitizenAPI.Application.Catalog.OrganizationUnits;

public class GetOrganizationUnitRequest : IRequest<Result<OrganizationUnitDetailsDto>>
{
    public Guid Id { get; set; }

    public GetOrganizationUnitRequest(Guid id) => Id = id;
}

public class OrganizationUnitByIdSpec : Specification<OrganizationUnit, OrganizationUnitDetailsDto>, ISingleResultSpecification
{
    public OrganizationUnitByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetOrganizationUnitRequestHandler : IRequestHandler<GetOrganizationUnitRequest, Result<OrganizationUnitDetailsDto>>
{
    private readonly IRepository<OrganizationUnit> _repository;
    private readonly IStringLocalizer<GetOrganizationUnitRequestHandler> _localizer;

    public GetOrganizationUnitRequestHandler(IRepository<OrganizationUnit> repository, IStringLocalizer<GetOrganizationUnitRequestHandler> localizer) => (_repository, _localizer) = (repository, localizer);

    public async Task<Result<OrganizationUnitDetailsDto>> Handle(GetOrganizationUnitRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetBySpecAsync(
            (ISpecification<OrganizationUnit, OrganizationUnitDetailsDto>)new OrganizationUnitByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["OrganizationUnit.notfound"], request.Id));
        return Result<OrganizationUnitDetailsDto>.Success(item);

    }
}