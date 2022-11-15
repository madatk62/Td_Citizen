namespace TD.CitizenAPI.Application.Catalog.AppConfigs;

public partial class CreateAppConfigRequest : IRequest<Result<Guid>>
{
    public string Key { get; set; } = default!;
    public string Value { get; set; } = default!;
    public string? Description { get; set; }
}

public class CreateAppConfigRequestValidator : CustomValidator<CreateAppConfigRequest>
{
    public CreateAppConfigRequestValidator(IReadRepository<AppConfig> repository, IStringLocalizer<CreateAppConfigRequestValidator> localizer) =>
         RuleFor(p => p.Key)
            .NotEmpty()
            .MaximumLength(512)
            .MustAsync(async (key, ct) => await repository.GetBySpecAsync(new AppConfigByNameSpec(key), ct) is null)
                .WithMessage((_, key) => string.Format(localizer["appconfig.alreadyexists"], key));
}

public class CreateAppConfigRequestHandler : IRequestHandler<CreateAppConfigRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<AppConfig> _repository;

    public CreateAppConfigRequestHandler(IRepositoryWithEvents<AppConfig> repository) => _repository = repository;

    public async Task<Result<Guid>> Handle(CreateAppConfigRequest request, CancellationToken cancellationToken)
    {
        var item = new AppConfig(request.Key, request.Value, request.Description);
        await _repository.AddAsync(item, cancellationToken);
        return Result<Guid>.Success(item.Id);
    }
}