namespace TD.CitizenAPI.Application.Catalog.OrganizationUnitUsers;

public class UpdateOrganizationUnitUserRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public Guid? OrganizationUnitId { get; set; }
    public string UserName { get; set; } = default!;
}

public class UpdateOrganizationUnitUserRequestValidator : CustomValidator<UpdateOrganizationUnitUserRequest>
{
    public UpdateOrganizationUnitUserRequestValidator(IRepository<OrganizationUnitUser> repository, IStringLocalizer<UpdateOrganizationUnitUserRequestValidator> localizer) =>
        RuleFor(p => p.UserName)
            .NotEmpty();
}

public class UpdateOrganizationUnitUserRequestHandler : IRequestHandler<UpdateOrganizationUnitUserRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<OrganizationUnitUser> _repository;
    private readonly IStringLocalizer<UpdateOrganizationUnitUserRequestHandler> _localizer;

    public UpdateOrganizationUnitUserRequestHandler(IRepositoryWithEvents<OrganizationUnitUser> repository, IStringLocalizer<UpdateOrganizationUnitUserRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Result<Guid>> Handle(UpdateOrganizationUnitUserRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(string.Format(_localizer["OrganizationUnitUser.notfound"], request.Id));

        item.Update(request.OrganizationUnitId,  request.UserName);

        await _repository.UpdateAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}