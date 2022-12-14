using TD.CitizenAPI.Domain.Common.Events;

namespace TD.CitizenAPI.Application.Catalog.Hotlines;

public class UpdateHotlineRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Address { get; set; }
    public string? Code { get; set; }
    public string? Detail { get; set; }
    public string? OtherDetail { get; set; }
    public string? Phone { get; set; }
    public string? Image { get; set; }
    public bool? Active { get; set; }
    public int? Order { get; set; }
    public Guid? HotlineCategoryId { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    public Guid? ProvinceId { get; set; }
    public Guid? DistrictId { get; set; }
    public Guid? CommuneId { get; set; }
    public string? Description { get; set; }
}

public class UpdateHotlineRequestValidator : CustomValidator<UpdateHotlineRequest>
{
    public UpdateHotlineRequestValidator(IRepository<Hotline> repository, IStringLocalizer<UpdateHotlineRequestValidator> localizer) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(256);
}

public class UpdateHotlineRequestHandler : IRequestHandler<UpdateHotlineRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Hotline> _repository;
    private readonly IStringLocalizer<UpdateHotlineRequestHandler> _localizer;

    public UpdateHotlineRequestHandler(IRepositoryWithEvents<Hotline> repository, IStringLocalizer<UpdateHotlineRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Result<Guid>> Handle(UpdateHotlineRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(string.Format(_localizer["hotline.notfound"], request.Id));

        item.Update(request.Name, request.Address, request.Code, request.Detail, request.OtherDetail, request.Phone, request.Image, request.Active, request.Order, request.HotlineCategoryId, request.Latitude, request.Longitude, request.ProvinceId, request.DistrictId, request.CommuneId, request.Description);
        item.DomainEvents.Add(EntityUpdatedEvent.WithEntity(item));

        await _repository.UpdateAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}