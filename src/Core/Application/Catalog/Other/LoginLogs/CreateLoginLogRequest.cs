namespace TD.CitizenAPI.Application.Catalog.LoginLogs;

public partial class CreateLoginLogRequest : IRequest<Result<Guid>>
{
    public string UserName { get; set; } = default!;
    public string? Ip { get; set; }
}


public class CreateLoginLogRequestHandler : IRequestHandler<CreateLoginLogRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<LoginLog> _repository;

    public CreateLoginLogRequestHandler(IRepositoryWithEvents<LoginLog> repository) => _repository = repository;

    public async Task<Result<Guid>> Handle(CreateLoginLogRequest request, CancellationToken cancellationToken)
    {
        var item = new LoginLog(request.UserName, request.Ip);
        await _repository.AddAsync(item, cancellationToken);
        return Result<Guid>.Success(item.Id);
    }
}