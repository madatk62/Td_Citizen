namespace TD.CitizenAPI.Application.Catalog.GovNewCategories;

public class UpdateGovNewCategoryRequest : IRequest<Result<Guid>>
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

public class UpdateGovNewCategoryRequestValidator : CustomValidator<UpdateGovNewCategoryRequest>
{
    public UpdateGovNewCategoryRequestValidator(IRepository<GovNewCategory> repository, IStringLocalizer<UpdateGovNewCategoryRequestValidator> localizer) =>
        RuleFor(p => p.Name)
            .NotEmpty();
}

public class UpdateGovNewCategoryRequestHandler : IRequestHandler<UpdateGovNewCategoryRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<GovNewCategory> _repository;
    private readonly IStringLocalizer<UpdateGovNewCategoryRequestHandler> _localizer;

    public UpdateGovNewCategoryRequestHandler(IRepositoryWithEvents<GovNewCategory> repository, IStringLocalizer<UpdateGovNewCategoryRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Result<Guid>> Handle(UpdateGovNewCategoryRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(string.Format(_localizer["GovNewCategory.notfound"], request.Id));

        item.Update(request.Name, request.Code, request.Icon, request.Image, request.CoverImage, request.Description, request.Order);

        await _repository.UpdateAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}