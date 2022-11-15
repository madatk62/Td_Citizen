namespace TD.CitizenAPI.Application.Catalog.ThoiGianThueNhas;

public partial class CreateThoiGianThueNhaRequest : IRequest<Result<Guid>>
{
    public string Name { get; set; } = default!;
    public string? Code { get; set; }
   
    public string? Description { get; set; }
}

public class CreateThoiGianThueNhaRequestValidator : CustomValidator<CreateThoiGianThueNhaRequest>
{
    public CreateThoiGianThueNhaRequestValidator(IReadRepository<ThoiGianThueNha> repository, IStringLocalizer<CreateThoiGianThueNhaRequestValidator> localizer) =>
        RuleFor(p => p.Name).NotEmpty();
}

public class CreateThoiGianThueNhaRequestHandler : IRequestHandler<CreateThoiGianThueNhaRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<ThoiGianThueNha> _repository;

    public CreateThoiGianThueNhaRequestHandler(IRepositoryWithEvents<ThoiGianThueNha> repository) => _repository = repository;

    public async Task<Result<Guid>> Handle(CreateThoiGianThueNhaRequest request, CancellationToken cancellationToken)
    {
        var item = new ThoiGianThueNha(request.Name, request.Code,request.Description);
        await _repository.AddAsync(item, cancellationToken);
        return Result<Guid>.Success(item.Id);
    }
}