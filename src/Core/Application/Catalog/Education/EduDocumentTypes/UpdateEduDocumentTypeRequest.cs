namespace TD.CitizenAPI.Application.Catalog.EduDocumentTypes;

public class UpdateEduDocumentTypeRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Image { get; set; }
    public string? Description { get; set; }
}

public class UpdateEduDocumentTypeRequestValidator : CustomValidator<UpdateEduDocumentTypeRequest>
{
    public UpdateEduDocumentTypeRequestValidator(IRepository<EduDocumentType> repository, IStringLocalizer<UpdateEduDocumentTypeRequestValidator> localizer) =>
        RuleFor(p => p.Name)
            .NotEmpty();
}

public class UpdateEduDocumentTypeRequestHandler : IRequestHandler<UpdateEduDocumentTypeRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<EduDocumentType> _repository;
    private readonly IStringLocalizer<UpdateEduDocumentTypeRequestHandler> _localizer;

    public UpdateEduDocumentTypeRequestHandler(IRepositoryWithEvents<EduDocumentType> repository, IStringLocalizer<UpdateEduDocumentTypeRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Result<Guid>> Handle(UpdateEduDocumentTypeRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(string.Format(_localizer["EduDocumentType.notfound"], request.Id));

        item.Update(request.Name, request.Code, request.Image, request.Description);

        await _repository.UpdateAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}