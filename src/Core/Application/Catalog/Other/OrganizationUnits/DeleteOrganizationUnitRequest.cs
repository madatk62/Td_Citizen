
namespace TD.CitizenAPI.Application.Catalog.OrganizationUnits;

public class DeleteOrganizationUnitRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }

    public DeleteOrganizationUnitRequest(Guid id) => Id = id;
}

public class DeleteOrganizationUnitRequestHandler : IRequestHandler<DeleteOrganizationUnitRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<OrganizationUnit> _OrganizationUnitRepo;
    private readonly IStringLocalizer<DeleteOrganizationUnitRequestHandler> _localizer;

    public DeleteOrganizationUnitRequestHandler(IRepositoryWithEvents<OrganizationUnit> OrganizationUnitRepo,  IStringLocalizer<DeleteOrganizationUnitRequestHandler> localizer) =>
        (_OrganizationUnitRepo,  _localizer) = (OrganizationUnitRepo,  localizer);

    public async Task<Result<Guid>> Handle(DeleteOrganizationUnitRequest request, CancellationToken cancellationToken)
    {


        var item = await _OrganizationUnitRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(_localizer["OrganizationUnit.notfound"]);

        await _OrganizationUnitRepo.DeleteAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}