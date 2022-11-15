namespace TD.CitizenAPI.Domain.Catalog;

public class LoginLog : AuditableEntity, IAggregateRoot
{
    public string UserName { get; set; }
    public string? Ip { get; set; }

    public LoginLog(string userName, string? ip)
    {
        UserName = userName;
        Ip = ip;
    }

}