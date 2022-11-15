namespace TD.CitizenAPI.Application.Catalog.OrganizationUnitUsers;

public class OrganizationUnitUserDto : IDto
{
    public Guid Id { get; set; }
    public Guid? OrganizationUnitId { get; set; }

    public string UserName { get; set; } = default!;

    public virtual OrganizationUnit? OrganizationUnit { get; set; }
}