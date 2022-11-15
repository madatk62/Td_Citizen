namespace TD.CitizenAPI.Application.Catalog.EduDocumentCategories;

public class UpdateEduDocumentCategoryRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Icon { get; set; }
    public string? Image { get; set; }
    public string? CoverImage { get; set; }
    public string? Description { get; set; }
    public int? Order { get; set; }
    public Guid? EduDocumentCatalogueId { get; set; }
}

public class UpdateEduDocumentCategoryRequestValidator : CustomValidator<UpdateEduDocumentCategoryRequest>
{
    public UpdateEduDocumentCategoryRequestValidator(IRepository<EduDocumentCategory> repository, IStringLocalizer<UpdateEduDocumentCategoryRequestValidator> localizer) =>
        RuleFor(p => p.Name)
            .NotEmpty();
}

public class UpdateEduDocumentCategoryRequestHandler : IRequestHandler<UpdateEduDocumentCategoryRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<EduDocumentCategory> _repository;
    private readonly IStringLocalizer<UpdateEduDocumentCategoryRequestHandler> _localizer;

    public UpdateEduDocumentCategoryRequestHandler(IRepositoryWithEvents<EduDocumentCategory> repository, IStringLocalizer<UpdateEduDocumentCategoryRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Result<Guid>> Handle(UpdateEduDocumentCategoryRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(string.Format(_localizer["EduDocumentCategory.notfound"], request.Id));

        item.Update(request.Name, request.Code, request.Icon, request.Image, request.CoverImage, request.Description, request.Order, request.EduDocumentCatalogueId);

        await _repository.UpdateAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}