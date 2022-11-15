using TD.CitizenAPI.Domain.Common.Events;

namespace TD.CitizenAPI.Application.Catalog.GovNews;

public class CreateGovNewRequest : IRequest<Result<Guid>>
{
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

public class CreateGovNewRequestValidator : CustomValidator<CreateGovNewRequest>
{
    public CreateGovNewRequestValidator(IReadRepository<GovNew> repository, IStringLocalizer<CreateGovNewRequestValidator> localizer) =>
        RuleFor(p => p.Title)
            .NotEmpty()
            .MaximumLength(256);
}

public class CreateMarketProductRequestHandler : IRequestHandler<CreateGovNewRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<GovNew> _repository;

    public CreateMarketProductRequestHandler(IRepositoryWithEvents<GovNew> repository) => _repository = repository;

    public async Task<Result<Guid>> Handle(CreateGovNewRequest request, CancellationToken cancellationToken)
    {
        var item = new GovNew(request.Title,request.Actor, request.Description, request.Content, request.Date, request.Image, request.Files, request.Source, 0, request.Level, request.IsStar, request.IsPublic, request.IsNotification, request.GovNewCategoryId, request.ProvinceId, request.DistrictId, request.CommuneId);
        item.DomainEvents.Add(EntityCreatedEvent.WithEntity(item));

        await _repository.AddAsync(item, cancellationToken);

        return Result<Guid>.Success(item.Id);
    }
}