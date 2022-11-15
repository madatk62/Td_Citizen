namespace TD.CitizenAPI.Application.Catalog.GovNews;

public class GetGovNewRequest : IRequest<Result<GovNewDetailsDto>>
{
    public Guid Id { get; set; }

    public GetGovNewRequest(Guid id) => Id = id;
}

public class GetGovNewRequestHandler : IRequestHandler<GetGovNewRequest, Result<GovNewDetailsDto>>
{
    private readonly IRepository<GovNew> _repository;
    private readonly IStringLocalizer<GetGovNewRequestHandler> _localizer;

    public GetGovNewRequestHandler(IRepository<GovNew> repository, IStringLocalizer<GetGovNewRequestHandler> localizer) => (_repository, _localizer) = (repository, localizer);

    public async Task<Result<GovNewDetailsDto>> Handle(GetGovNewRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetBySpecAsync(
            (ISpecification<GovNew, GovNewDetailsDto>)new GovNewByIdWithIncludeSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["GovNew.notfound"], request.Id));
        return Result<GovNewDetailsDto>.Success(item);

    }
}