namespace TD.CitizenAPI.Application.Catalog.OrganizationUnitUsers;

public class GetOrganizationUnitUserRequest : IRequest<Result<OrganizationUnitUserDetailsDto>>
{
    public Guid Id { get; set; }

    public GetOrganizationUnitUserRequest(Guid id) => Id = id;
}

public class OrganizationUnitUserByIdSpec : Specification<OrganizationUnitUser, OrganizationUnitUserDetailsDto>, ISingleResultSpecification
{
    public OrganizationUnitUserByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetOrganizationUnitUserRequestHandler : IRequestHandler<GetOrganizationUnitUserRequest, Result<OrganizationUnitUserDetailsDto>>
{
    private readonly IRepository<OrganizationUnitUser> _repository;
    private readonly IStringLocalizer<GetOrganizationUnitUserRequestHandler> _localizer;

    public GetOrganizationUnitUserRequestHandler(IRepository<OrganizationUnitUser> repository, IStringLocalizer<GetOrganizationUnitUserRequestHandler> localizer) => (_repository, _localizer) = (repository, localizer);

    public async Task<Result<OrganizationUnitUserDetailsDto>> Handle(GetOrganizationUnitUserRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetBySpecAsync(
            (ISpecification<OrganizationUnitUser, OrganizationUnitUserDetailsDto>)new OrganizationUnitUserByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["OrganizationUnitUser.notfound"], request.Id));
        return Result<OrganizationUnitUserDetailsDto>.Success(item);

    }
}