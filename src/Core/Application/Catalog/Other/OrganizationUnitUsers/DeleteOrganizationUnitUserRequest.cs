
namespace TD.CitizenAPI.Application.Catalog.OrganizationUnitUsers;

public class DeleteOrganizationUnitUserRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }

    public DeleteOrganizationUnitUserRequest(Guid id) => Id = id;
}

public class DeleteOrganizationUnitUserRequestHandler : IRequestHandler<DeleteOrganizationUnitUserRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<OrganizationUnitUser> _OrganizationUnitUserRepo;
    private readonly IStringLocalizer<DeleteOrganizationUnitUserRequestHandler> _localizer;

    public DeleteOrganizationUnitUserRequestHandler(IRepositoryWithEvents<OrganizationUnitUser> OrganizationUnitUserRepo,  IStringLocalizer<DeleteOrganizationUnitUserRequestHandler> localizer) =>
        (_OrganizationUnitUserRepo,  _localizer) = (OrganizationUnitUserRepo,  localizer);

    public async Task<Result<Guid>> Handle(DeleteOrganizationUnitUserRequest request, CancellationToken cancellationToken)
    {


        var item = await _OrganizationUnitUserRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(_localizer["OrganizationUnitUser.notfound"]);

        await _OrganizationUnitUserRepo.DeleteAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}