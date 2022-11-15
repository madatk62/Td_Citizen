using TD.CitizenAPI.Domain.Common.Events;

namespace TD.CitizenAPI.Application.Catalog.EduDocuments;

public class CreateEduDocumentRequest : IRequest<Result<Guid>>
{
    public string Name { get; set; } = default!;
    public string? Image { get; set; }
    public string? File { get; set; }
    public string? Tags { get; set; }
    public string? Description { get; set; }

    public bool? IsStar { get; set; }
    public bool? IsPublic { get; set; }


    public Guid? EduDocumentCategoryId { get; set; }
    public Guid? EduDocumentTypeId { get; set; }

}

public class CreateEduDocumentRequestValidator : CustomValidator<CreateEduDocumentRequest>
{
    public CreateEduDocumentRequestValidator(IReadRepository<EduDocument> repository, IStringLocalizer<CreateEduDocumentRequestValidator> localizer) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(512);
}

public class CreateMarketProductRequestHandler : IRequestHandler<CreateEduDocumentRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<EduDocument> _repository;

    public CreateMarketProductRequestHandler(IRepositoryWithEvents<EduDocument> repository) => _repository = repository;

    public async Task<Result<Guid>> Handle(CreateEduDocumentRequest request, CancellationToken cancellationToken)
    {
        var item = new EduDocument(request.Name,request.Image, request.File, request.Tags, request.Description, 0,0,request.IsStar, request.IsPublic, request.EduDocumentCategoryId, request.EduDocumentTypeId);
        item.DomainEvents.Add(EntityCreatedEvent.WithEntity(item));

        await _repository.AddAsync(item, cancellationToken);

        return Result<Guid>.Success(item.Id);
    }
}