namespace TD.CitizenAPI.Application.Catalog.OrganizationUnits;

public class UpdateOrganizationUnitRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public Guid? ParentId { get; set; }
    public Guid? AreaId { get; set; }

    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Code { get; set; }

    public string? FullCode { get; set; }
    public string? ParentCode { get; set; }

    public string? Type { get; set; }
}

public class UpdateOrganizationUnitRequestValidator : CustomValidator<UpdateOrganizationUnitRequest>
{
    public UpdateOrganizationUnitRequestValidator(IRepository<OrganizationUnit> repository, IStringLocalizer<UpdateOrganizationUnitRequestValidator> localizer) =>
         RuleFor(p => p.Code)
            .NotEmpty()
            .MaximumLength(512)
            .MustAsync(async (key, ct) => await repository.GetBySpecAsync(new OrganizationUnitByCodeSpec(key), ct) is null)
                .WithMessage((_, key) => string.Format(localizer["OrganizationUnit.alreadyexists"], key));
}

public class UpdateOrganizationUnitRequestHandler : IRequestHandler<UpdateOrganizationUnitRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<OrganizationUnit> _repository;
    private readonly IStringLocalizer<UpdateOrganizationUnitRequestHandler> _localizer;

    public UpdateOrganizationUnitRequestHandler(IRepositoryWithEvents<OrganizationUnit> repository, IStringLocalizer<UpdateOrganizationUnitRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Result<Guid>> Handle(UpdateOrganizationUnitRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(string.Format(_localizer["OrganizationUnit.notfound"], request.Id));

        item.Update(request.ParentId, request.AreaId, request.Name, request.Description, request.Code, request.FullCode, request.ParentCode, request.Type);

        await _repository.UpdateAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}