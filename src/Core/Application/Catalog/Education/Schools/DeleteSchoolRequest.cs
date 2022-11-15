namespace TD.CitizenAPI.Application.Catalog.Schools;

public class DeleteSchoolRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }

    public DeleteSchoolRequest(Guid id) => Id = id;
}

public class DeleteSchoolRequestHandler : IRequestHandler<DeleteSchoolRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<School> _SchoolRepo;
    private readonly IStringLocalizer<DeleteSchoolRequestHandler> _localizer;

    public DeleteSchoolRequestHandler(IRepositoryWithEvents<School> SchoolRepo, IStringLocalizer<DeleteSchoolRequestHandler> localizer) =>
        (_SchoolRepo, _localizer) = (SchoolRepo, localizer);

    public async Task<Result<Guid>> Handle(DeleteSchoolRequest request, CancellationToken cancellationToken)
    {


        var item = await _SchoolRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(_localizer["School.notfound"]);

        await _SchoolRepo.DeleteAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}