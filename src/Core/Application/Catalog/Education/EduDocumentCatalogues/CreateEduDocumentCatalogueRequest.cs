namespace TD.CitizenAPI.Application.Catalog.EduDocumentCatalogues;

public partial class CreateEduDocumentCatalogueRequest : IRequest<Result<Guid>>
{
    public string Name { get; set; } = default!;
    public string Code { get; set; } = default!;
    public string? Icon { get; set; }
    public string? Image { get; set; }
    public string? CoverImage { get; set; }
    public string? Description { get; set; }
    public int? Order { get; set; }
}

public class CreateEduDocumentCatalogueRequestValidator : CustomValidator<CreateEduDocumentCatalogueRequest>
{
    public CreateEduDocumentCatalogueRequestValidator(IReadRepository<EduDocumentCatalogue> repository, IStringLocalizer<CreateEduDocumentCatalogueRequestValidator> localizer) =>
        RuleFor(p => p.Name).NotEmpty();
}

public class CreateEduDocumentCatalogueRequestHandler : IRequestHandler<CreateEduDocumentCatalogueRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<EduDocumentCatalogue> _repository;

    public CreateEduDocumentCatalogueRequestHandler(IRepositoryWithEvents<EduDocumentCatalogue> repository) => _repository = repository;

    public async Task<Result<Guid>> Handle(CreateEduDocumentCatalogueRequest request, CancellationToken cancellationToken)
    {
        var item = new EduDocumentCatalogue(request.Name, request.Code, request.Icon, request.Image, request.CoverImage, request.Description, request.Order);
        await _repository.AddAsync(item, cancellationToken);
        return Result<Guid>.Success(item.Id);
    }
}