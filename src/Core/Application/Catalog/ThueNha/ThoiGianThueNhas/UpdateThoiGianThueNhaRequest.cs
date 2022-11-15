namespace TD.CitizenAPI.Application.Catalog.ThoiGianThueNhas;

public class UpdateThoiGianThueNhaRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Code { get; set; }
   
    public string? Description { get; set; }
}

public class UpdateThoiGianThueNhaRequestValidator : CustomValidator<UpdateThoiGianThueNhaRequest>
{
    public UpdateThoiGianThueNhaRequestValidator(IRepository<ThoiGianThueNha> repository, IStringLocalizer<UpdateThoiGianThueNhaRequestValidator> localizer) =>
        RuleFor(p => p.Name)
            .NotEmpty();
}

public class UpdateThoiGianThueNhaRequestHandler : IRequestHandler<UpdateThoiGianThueNhaRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<ThoiGianThueNha> _repository;
    private readonly IStringLocalizer<UpdateThoiGianThueNhaRequestHandler> _localizer;

    public UpdateThoiGianThueNhaRequestHandler(IRepositoryWithEvents<ThoiGianThueNha> repository, IStringLocalizer<UpdateThoiGianThueNhaRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Result<Guid>> Handle(UpdateThoiGianThueNhaRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(string.Format(_localizer["ThoiGianThueNha.notfound"], request.Id));

        item.Update(request.Name, request.Code,  request.Description);

        await _repository.UpdateAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}