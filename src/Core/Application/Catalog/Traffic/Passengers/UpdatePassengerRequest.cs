namespace TD.CitizenAPI.Application.Catalog.Passengers;

public class UpdatePassengerRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}

public class UpdatePassengerRequestValidator : CustomValidator<UpdatePassengerRequest>
{
    public UpdatePassengerRequestValidator(IRepository<Passenger> repository, IStringLocalizer<UpdatePassengerRequestValidator> localizer) =>
        RuleFor(p => p.Name)
            .NotEmpty();
}

public class UpdatePassengerRequestHandler : IRequestHandler<UpdatePassengerRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Passenger> _repository;
    private readonly IStringLocalizer<UpdatePassengerRequestHandler> _localizer;

    public UpdatePassengerRequestHandler(IRepositoryWithEvents<Passenger> repository, IStringLocalizer<UpdatePassengerRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Result<Guid>> Handle(UpdatePassengerRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(string.Format(_localizer["marketcategory.notfound"], request.Id));

        item.Update(request.Name, request.Email, request.Phone);

        await _repository.UpdateAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}