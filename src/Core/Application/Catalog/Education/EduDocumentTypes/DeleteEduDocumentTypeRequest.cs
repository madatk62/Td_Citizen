using TD.CitizenAPI.Application.Catalog.Hotlines;

namespace TD.CitizenAPI.Application.Catalog.EduDocumentTypes;

public class DeleteEduDocumentTypeRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }

    public DeleteEduDocumentTypeRequest(Guid id) => Id = id;
}

public class DeleteEduDocumentTypeRequestHandler : IRequestHandler<DeleteEduDocumentTypeRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<EduDocumentType> _eduDocumentTypeRepo;
    private readonly IStringLocalizer<DeleteEduDocumentTypeRequestHandler> _localizer;

    public DeleteEduDocumentTypeRequestHandler(IRepositoryWithEvents<EduDocumentType> EduDocumentTypeRepo,IStringLocalizer<DeleteEduDocumentTypeRequestHandler> localizer) =>
        (_eduDocumentTypeRepo,  _localizer) = (EduDocumentTypeRepo,  localizer);

    public async Task<Result<Guid>> Handle(DeleteEduDocumentTypeRequest request, CancellationToken cancellationToken)
    {
       
        var item = await _eduDocumentTypeRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(_localizer["EduDocumentType.notfound"]);

        await _eduDocumentTypeRepo.DeleteAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}