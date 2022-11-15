﻿
namespace TD.CitizenAPI.Application.Catalog.ThoiGianThueNhas;

public class DeleteThoiGianThueNhaRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }

    public DeleteThoiGianThueNhaRequest(Guid id) => Id = id;
}

public class DeleteThoiGianThueNhaRequestHandler : IRequestHandler<DeleteThoiGianThueNhaRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<ThoiGianThueNha> _repo;
    private readonly IStringLocalizer<DeleteThoiGianThueNhaRequestHandler> _localizer;

    public DeleteThoiGianThueNhaRequestHandler(IRepositoryWithEvents<ThoiGianThueNha> repo, IStringLocalizer<DeleteThoiGianThueNhaRequestHandler> localizer) =>
        (_repo,  _localizer) = (repo, localizer);

    public async Task<Result<Guid>> Handle(DeleteThoiGianThueNhaRequest request, CancellationToken cancellationToken)
    {
        

        var item = await _repo.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(_localizer["ThoiGianThueNha.notfound"]);

        await _repo.DeleteAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}