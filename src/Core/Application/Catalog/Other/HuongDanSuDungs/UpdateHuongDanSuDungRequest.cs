namespace TD.CitizenAPI.Application.Catalog.HuongDanSuDungs;

public class UpdateHuongDanSuDungRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? File { get; set; }
    public string? Description { get; set; }
}

public class UpdateHuongDanSuDungRequestValidator : CustomValidator<UpdateHuongDanSuDungRequest>
{
    public UpdateHuongDanSuDungRequestValidator(IRepository<HuongDanSuDung> repository, IStringLocalizer<UpdateHuongDanSuDungRequestValidator> localizer) =>
        RuleFor(p => p.Name)
            .NotEmpty();
}

public class UpdateHuongDanSuDungRequestHandler : IRequestHandler<UpdateHuongDanSuDungRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<HuongDanSuDung> _repository;
    private readonly IStringLocalizer<UpdateHuongDanSuDungRequestHandler> _localizer;

    public UpdateHuongDanSuDungRequestHandler(IRepositoryWithEvents<HuongDanSuDung> repository, IStringLocalizer<UpdateHuongDanSuDungRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Result<Guid>> Handle(UpdateHuongDanSuDungRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(string.Format(_localizer["HuongDanSuDung.notfound"], request.Id));

        item.Update(request.Name,  request.File, request.Description);

        await _repository.UpdateAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}