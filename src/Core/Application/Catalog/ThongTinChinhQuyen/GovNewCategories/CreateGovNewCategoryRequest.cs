namespace TD.CitizenAPI.Application.Catalog.GovNewCategories;

public partial class CreateGovNewCategoryRequest : IRequest<Result<Guid>>
{
    public string Name { get; set; } = default!;
    public string Code { get; set; } = default!;
    public string? Icon { get; set; }
    public string? Image { get; set; }
    public string? CoverImage { get; set; }
    public string? Description { get; set; }
    public int? Order { get; set; }
}

public class CreateGovNewCategoryRequestValidator : CustomValidator<CreateGovNewCategoryRequest>
{
    public CreateGovNewCategoryRequestValidator(IReadRepository<GovNewCategory> repository, IStringLocalizer<CreateGovNewCategoryRequestValidator> localizer) =>
        RuleFor(p => p.Name).NotEmpty();
}

public class CreateGovNewCategoryRequestHandler : IRequestHandler<CreateGovNewCategoryRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<GovNewCategory> _repository;

    public CreateGovNewCategoryRequestHandler(IRepositoryWithEvents<GovNewCategory> repository) => _repository = repository;

    public async Task<Result<Guid>> Handle(CreateGovNewCategoryRequest request, CancellationToken cancellationToken)
    {
        var item = new GovNewCategory(request.Name, request.Code, request.Icon, request.Image, request.CoverImage, request.Description, request.Order);
        await _repository.AddAsync(item, cancellationToken);
        return Result<Guid>.Success(item.Id);
    }
}