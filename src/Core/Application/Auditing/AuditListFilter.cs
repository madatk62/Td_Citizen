namespace TD.CitizenAPI.Application.Auditing;

public class AuditListFilter : PaginationFilter
{
    public string? UserId { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}