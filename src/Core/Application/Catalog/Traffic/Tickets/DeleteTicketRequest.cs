using TD.CitizenAPI.Application.Catalog.Carpools;

namespace TD.CitizenAPI.Application.Catalog.Tickets;

public class DeleteTicketRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }

    public DeleteTicketRequest(Guid id) => Id = id;
}

public class DeleteTicketRequestHandler : IRequestHandler<DeleteTicketRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Ticket> _repository;
    private readonly IReadRepository<Carpool> _carpoolRepo;
    private readonly IStringLocalizer<DeleteTicketRequestHandler> _localizer;

    public DeleteTicketRequestHandler(IRepositoryWithEvents<Ticket> repository, IReadRepository<Carpool> carpoolRepo, IStringLocalizer<DeleteTicketRequestHandler> localizer) =>
        (_repository, _carpoolRepo, _localizer) = (repository, carpoolRepo, localizer);

    public async Task<Result<Guid>> Handle(DeleteTicketRequest request, CancellationToken cancellationToken)
    {


        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(_localizer["Ticket.notfound"]);

        await _repository.DeleteAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}