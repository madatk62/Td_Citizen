using TD.CitizenAPI.Domain.Common.Events;

namespace TD.CitizenAPI.Application.Catalog.GovNews;

public class UpdateGovNewRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Actor { get; set; }
    public string? Description { get; set; }

    public string? Content { get; set; }
    public DateTime? Date { get; set; }
    public DateTime? CreatedOn { get; set; }
    public string? Image { get; set; }
    public string? Files { get; set; }
    public string? Source { get; set; }
    public int? Level { get; set; }

    public bool? IsStar { get; set; }
    public bool? IsPublic { get; set; }
    public bool? IsNotification { get; set; }


    public Guid? GovNewCategoryId { get; set; }
    public Guid? ProvinceId { get; set; }
    public Guid? DistrictId { get; set; }
    public Guid? CommuneId { get; set; }
}

public class UpdateGovNewRequestValidator : CustomValidator<UpdateGovNewRequest>
{
    public UpdateGovNewRequestValidator(IRepository<GovNew> repository, IStringLocalizer<UpdateGovNewRequestValidator> localizer) =>
        RuleFor(p => p.Title)
            .NotEmpty()
            .MaximumLength(512);
}

public class UpdateGovNewRequestHandler : IRequestHandler<UpdateGovNewRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<GovNew> _repository;
    private readonly IStringLocalizer<UpdateGovNewRequestHandler> _localizer;

    public UpdateGovNewRequestHandler(IRepositoryWithEvents<GovNew> repository, IStringLocalizer<UpdateGovNewRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Result<Guid>> Handle(UpdateGovNewRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(string.Format(_localizer["GovNew.notfound"], request.Id));

        item.Update(request.Title, request.Actor, request.Description, request.Content, request.Date, request.Image, request.Files, request.Source,  request.Level, request.IsStar, request.IsPublic, request.IsNotification, request.GovNewCategoryId, request.ProvinceId, request.DistrictId, request.CommuneId);
        item.DomainEvents.Add(EntityUpdatedEvent.WithEntity(item));

        await _repository.UpdateAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}