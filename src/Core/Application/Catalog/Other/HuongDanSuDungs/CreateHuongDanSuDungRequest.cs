namespace TD.CitizenAPI.Application.Catalog.HuongDanSuDungs;

public partial class CreateHuongDanSuDungRequest : IRequest<Result<Guid>>
{
    public string Name { get; set; } = default!;
    public string? File { get; set; }
    public string? Description { get; set; }
}

public class CreateHuongDanSuDungRequestValidator : CustomValidator<CreateHuongDanSuDungRequest>
{
    public CreateHuongDanSuDungRequestValidator(IReadRepository<HuongDanSuDung> repository, IStringLocalizer<CreateHuongDanSuDungRequestValidator> localizer) =>
        RuleFor(p => p.Name).NotEmpty();
}

public class CreateHuongDanSuDungRequestHandler : IRequestHandler<CreateHuongDanSuDungRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<HuongDanSuDung> _repository;

    public CreateHuongDanSuDungRequestHandler(IRepositoryWithEvents<HuongDanSuDung> repository) => _repository = repository;

    public async Task<Result<Guid>> Handle(CreateHuongDanSuDungRequest request, CancellationToken cancellationToken)
    {
        var item = new HuongDanSuDung(request.Name, request.File, request.Description);
        await _repository.AddAsync(item, cancellationToken);
        return Result<Guid>.Success(item.Id);
    }
}