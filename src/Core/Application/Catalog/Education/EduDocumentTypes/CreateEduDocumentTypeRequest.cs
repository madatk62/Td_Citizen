namespace TD.CitizenAPI.Application.Catalog.EduDocumentTypes;

public partial class CreateEduDocumentTypeRequest : IRequest<Result<Guid>>
{
    public string Name { get; set; } = default!;
    public string Code { get; set; } = default!;
    public string? Image { get; set; }
    public string? Description { get; set; }
}

public class CreateEduDocumentTypeRequestValidator : CustomValidator<CreateEduDocumentTypeRequest>
{
    public CreateEduDocumentTypeRequestValidator(IReadRepository<EduDocumentType> repository, IStringLocalizer<CreateEduDocumentTypeRequestValidator> localizer) =>
        RuleFor(p => p.Name).NotEmpty();
}

public class CreateEduDocumentTypeRequestHandler : IRequestHandler<CreateEduDocumentTypeRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<EduDocumentType> _repository;

    public CreateEduDocumentTypeRequestHandler(IRepositoryWithEvents<EduDocumentType> repository) => _repository = repository;

    public async Task<Result<Guid>> Handle(CreateEduDocumentTypeRequest request, CancellationToken cancellationToken)
    {
        var item = new EduDocumentType(request.Name, request.Code, request.Image, request.Description);
        await _repository.AddAsync(item, cancellationToken);
        return Result<Guid>.Success(item.Id);
    }
}