using TD.CitizenAPI.Domain.Common.Events;

namespace TD.CitizenAPI.Application.Catalog.EduDocuments;

public class UpdateEduDocumentRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
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

public class UpdateEduDocumentRequestValidator : CustomValidator<UpdateEduDocumentRequest>
{
    public UpdateEduDocumentRequestValidator(IRepository<EduDocument> repository, IStringLocalizer<UpdateEduDocumentRequestValidator> localizer) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(256);
}

public class UpdateEduDocumentRequestHandler : IRequestHandler<UpdateEduDocumentRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<EduDocument> _repository;
    private readonly IStringLocalizer<UpdateEduDocumentRequestHandler> _localizer;

    public UpdateEduDocumentRequestHandler(IRepositoryWithEvents<EduDocument> repository, IStringLocalizer<UpdateEduDocumentRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Result<Guid>> Handle(UpdateEduDocumentRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(string.Format(_localizer["EduDocument.notfound"], request.Id));

        item.Update(request.Name, request.Image, request.File, request.Tags, request.Description, request.IsStar, request.IsPublic, request.EduDocumentCategoryId, request.EduDocumentTypeId);
        item.DomainEvents.Add(EntityUpdatedEvent.WithEntity(item));

        await _repository.UpdateAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}