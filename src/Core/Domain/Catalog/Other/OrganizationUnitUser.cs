namespace TD.CitizenAPI.Domain.Catalog;

public class OrganizationUnitUser : AuditableEntity, IAggregateRoot
{
    public Guid? OrganizationUnitId { get; set; }

    public string UserName { get;  set; }
   
    public virtual OrganizationUnit? OrganizationUnit { get; set; }

    public OrganizationUnitUser(Guid? organizationUnitId, string userName)
    {
        OrganizationUnitId = organizationUnitId;
        UserName = userName;
       
    }

    public OrganizationUnitUser Update(Guid? organizationUnitId, string userName)
    {
        if (userName is not null && UserName?.Equals(userName) is not true) UserName = userName;

        if (organizationUnitId.HasValue && organizationUnitId.Value != Guid.Empty && !OrganizationUnitId.Equals(organizationUnitId.Value)) OrganizationUnitId = organizationUnitId.Value;

        return this;
    }
}