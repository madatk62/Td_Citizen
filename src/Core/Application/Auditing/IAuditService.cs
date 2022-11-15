namespace TD.CitizenAPI.Application.Auditing;

public interface IAuditService : ITransientService
{
    Task<List<AuditDto>> GetUserTrailsAsync(string? userId);
    Task<PaginationResponse<AuditDto>> SearchAsync(AuditListFilter filter, CancellationToken cancellationToken);

    Task<bool> DeleteAsync(Guid? id, CancellationToken cancellationToken);

}