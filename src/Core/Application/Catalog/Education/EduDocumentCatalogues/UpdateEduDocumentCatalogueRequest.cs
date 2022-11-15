namespace TD.CitizenAPI.Application.Catalog.EduDocumentCatalogues;

public class UpdateEduDocumentCatalogueRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Icon { get; set; }
    public string? Image { get; set; }
    public string? CoverImage { get; set; }
    public string? Description { get; set; }
    public int? Order { get; set; }
}

public class UpdateEduDocumentCatalogueRequestValidator : CustomValidator<UpdateEduDocumentCatalogueRequest>
{
    public UpdateEduDocumentCatalogueRequestValidator(IRepository<EduDocumentCatalogue> repository, IStringLocalizer<UpdateEduDocumentCatalogueRequestValidator> localizer) =>
        RuleFor(p => p.Name)
            .NotEmpty();
}

public class UpdateEduDocumentCatalogueRequestHandler : IRequestHandler<UpdateEduDocumentCatalogueRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<EduDocumentCatalogue> _repository;
    private readonly IStringLocalizer<UpdateEduDocumentCatalogueRequestHandler> _localizer;

    public UpdateEduDocumentCatalogueRequestHandler(IRepositoryWithEvents<EduDocumentCatalogue> repository, IStringLocalizer<UpdateEduDocumentCatalogueRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Result<Guid>> Handle(UpdateEduDocumentCatalogueRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(string.Format(_localizer["EduDocumentCatalogue.notfound"], request.Id));

        item.Update(request.Name, request.Code, request.Icon, request.Image, request.CoverImage, request.Description, request.Order);

        await _repository.UpdateAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}