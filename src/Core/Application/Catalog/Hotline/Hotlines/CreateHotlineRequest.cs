using TD.CitizenAPI.Domain.Common.Events;

namespace TD.CitizenAPI.Application.Catalog.Hotlines;

public class CreateHotlineRequest : IRequest<Result<Guid>>
{
    public string Name { get; set; } = default!;
    public string? Address { get; set; }
    public string? Code { get; set; }
    public string? Detail { get; set; }
    public string? OtherDetail { get; set; }
    public string? Phone { get; set; }
    public string? Image { get; set; }
    public int? Order { get; set; }
    public Guid? HotlineCategoryId { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    public Guid? ProvinceId { get; set; }
    public Guid? DistrictId { get; set; }
    public Guid? CommuneId { get; set; }
    public string? Description { get; set; }

}

public class CreateHotlineRequestValidator : CustomValidator<CreateHotlineRequest>
{
    public CreateHotlineRequestValidator(IReadRepository<Hotline> repository, IStringLocalizer<CreateHotlineRequestValidator> localizer) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(256);
}

public class CreateMarketProductRequestHandler : IRequestHandler<CreateHotlineRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Hotline> _repository;

    public CreateMarketProductRequestHandler(IRepositoryWithEvents<Hotline> repository) => _repository = repository;

    public async Task<Result<Guid>> Handle(CreateHotlineRequest request, CancellationToken cancellationToken)
    {
        var item = new Hotline(request.Name, request.Address, request.Code, request.Detail, request.OtherDetail, request.Phone, request.Image, true, request.Order, request.HotlineCategoryId, request.Latitude, request.Longitude, request.ProvinceId, request.DistrictId, request.CommuneId, request.Description);
        item.DomainEvents.Add(EntityCreatedEvent.WithEntity(item));

        await _repository.AddAsync(item, cancellationToken);

        return Result<Guid>.Success(item.Id);
    }
}