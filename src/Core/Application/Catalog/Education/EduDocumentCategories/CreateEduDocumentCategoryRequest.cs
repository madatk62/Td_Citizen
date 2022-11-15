namespace TD.CitizenAPI.Application.Catalog.EduDocumentCategories;

public class CreateEduDocumentCategoryRequest : IRequest<Result<Guid>>
{
    public string Name { get; set; } = default!;
    public string Code { get; set; } = default!;
    public string? Icon { get; set; }
    public string? Image { get; set; }
    public string? CoverImage { get; set; }
    public string? Description { get; set; }
    public int? Order { get; set; }
    public Guid? EduDocumentCatalogueId { get; set; }
}

public class CreateEduDocumentCategoryRequestValidator : CustomValidator<CreateEduDocumentCategoryRequest>
{
    public CreateEduDocumentCategoryRequestValidator(IReadRepository<EduDocumentCategory> repository, IStringLocalizer<CreateEduDocumentCategoryRequestValidator> localizer) =>
        RuleFor(p => p.Name).NotEmpty();
}

public class CreateEduDocumentCategoryRequestHandler : IRequestHandler<CreateEduDocumentCategoryRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<EduDocumentCategory> _repository;

    public CreateEduDocumentCategoryRequestHandler(IRepositoryWithEvents<EduDocumentCategory> repository) => _repository = repository;

    public async Task<Result<Guid>> Handle(CreateEduDocumentCategoryRequest request, CancellationToken cancellationToken)
    {
        var item = new EduDocumentCategory(request.Name, request.Code, request.Icon, request.Image, request.CoverImage, request.Description, request.Order, request.EduDocumentCatalogueId);
        await _repository.AddAsync(item, cancellationToken);
        return Result<Guid>.Success(item.Id);
    }
}