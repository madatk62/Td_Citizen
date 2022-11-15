namespace TD.CitizenAPI.Application.Catalog.OrganizationUnits;

public partial class CreateOrganizationUnitRequest : IRequest<Result<Guid>>
{
    public Guid? ParentId { get; set; }
    public Guid? AreaId { get; set; }

    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Code { get; set; }

    public string? FullCode { get; set; }
    public string? ParentCode { get; set; }

    public string? Type { get; set; }
}

public class CreateOrganizationUnitRequestValidator : CustomValidator<CreateOrganizationUnitRequest>
{
    public CreateOrganizationUnitRequestValidator(IReadRepository<OrganizationUnit> repository, IStringLocalizer<CreateOrganizationUnitRequestValidator> localizer) =>
        RuleFor(p => p.Code)
            .NotEmpty()
            .MaximumLength(512)
            .MustAsync(async (key, ct) => await repository.GetBySpecAsync(new OrganizationUnitByCodeSpec(key), ct) is null)
            .WithMessage((_, key) => string.Format(localizer["OrganizationUnit.alreadyexists"], key));
}

public class CreateOrganizationUnitRequestHandler : IRequestHandler<CreateOrganizationUnitRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<OrganizationUnit> _repository;

    public CreateOrganizationUnitRequestHandler(IRepositoryWithEvents<OrganizationUnit> repository) => _repository = repository;

    public async Task<Result<Guid>> Handle(CreateOrganizationUnitRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Code) || string.IsNullOrEmpty(request.Name))
        {
            throw new NotFoundException(string.Format("OrganizationUnit.notfound", ""));
        }

        var items = await _repository.ListAsync(new OrganizationUnitByCodeSpec(request.Code), cancellationToken);
        if (items.Any())
        {
            throw new NotFoundException(string.Format("OrganizationUnit.alreadyexists", request.Code));

        }

        var item = new OrganizationUnit(request.ParentId, request.AreaId, request.Name,request.Description,request.Code,request.FullCode,request.ParentCode,request.Type);
        await _repository.AddAsync(item, cancellationToken);
        return Result<Guid>.Success(item.Id);
    }
}