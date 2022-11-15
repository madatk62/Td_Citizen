namespace TD.CitizenAPI.Application.Catalog.AppConfigs;

public class UpdateAppConfigRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string Key { get; set; } = default!;
    public string Value { get; set; } = default!;
    public string? Description { get; set; }
}

public class UpdateAppConfigRequestValidator : CustomValidator<UpdateAppConfigRequest>
{
    public UpdateAppConfigRequestValidator(IRepository<AppConfig> repository, IStringLocalizer<UpdateAppConfigRequestValidator> localizer) =>
        RuleFor(p => p.Key)
            .NotEmpty()
            .MaximumLength(512)
            .MustAsync(async (key, ct) => await repository.GetBySpecAsync(new AppConfigByNameSpec(key), ct) is null)
                .WithMessage((_, key) => string.Format(localizer["appconfig.alreadyexists"], key));
}

public class UpdateAppConfigRequestHandler : IRequestHandler<UpdateAppConfigRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<AppConfig> _repository;
    private readonly IStringLocalizer<UpdateAppConfigRequestHandler> _localizer;

    public UpdateAppConfigRequestHandler(IRepositoryWithEvents<AppConfig> repository, IStringLocalizer<UpdateAppConfigRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Result<Guid>> Handle(UpdateAppConfigRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(string.Format(_localizer["AppConfig.notfound"], request.Id));

        item.Update(request.Key,  request.Value, request.Description);

        await _repository.UpdateAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}