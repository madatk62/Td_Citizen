namespace TD.CitizenAPI.Application.Catalog.OrganizationUnitUsers;

public partial class CreateOrganizationUnitUserRequest : IRequest<Result<Guid>>
{
    public Guid? OrganizationUnitId { get; set; }

    public string UserName { get; set; } = default!;
}

public class CreateOrganizationUnitUserRequestValidator : CustomValidator<CreateOrganizationUnitUserRequest>
{
    public CreateOrganizationUnitUserRequestValidator(IReadRepository<OrganizationUnitUser> repository, IStringLocalizer<CreateOrganizationUnitUserRequestValidator> localizer) =>
        RuleFor(p => p.UserName).NotEmpty();
}

public class CreateOrganizationUnitUserRequestHandler : IRequestHandler<CreateOrganizationUnitUserRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<OrganizationUnitUser> _repository;

    public CreateOrganizationUnitUserRequestHandler(IRepositoryWithEvents<OrganizationUnitUser> repository) => _repository = repository;

    public async Task<Result<Guid>> Handle(CreateOrganizationUnitUserRequest request, CancellationToken cancellationToken)
    {
        var item = new OrganizationUnitUser(request.OrganizationUnitId, request.UserName);
        await _repository.AddAsync(item, cancellationToken);
        return Result<Guid>.Success(item.Id);
    }
}