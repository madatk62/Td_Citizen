namespace TD.CitizenAPI.Domain.Catalog;

public class OrganizationUnit : AuditableEntity, IAggregateRoot
{
    public Guid? ParentId { get; set; }

    public string Name { get;  set; }
    public string? Description { get;  set; }
    public string? Code { get; set; }

    public string? FullCode { get; set; }
    public string? ParentCode { get; set; }

    public string? Type { get; set; }
    public int? Order { get; set; }

    //Dia Ban
    public Guid? AreaId { get; set; }

    public virtual Area? Area { get; set; }

    public virtual OrganizationUnit? Parent { get; set; }

    public OrganizationUnit(Guid? parentId, Guid? areaId, string name, string? description, string? code, string? fullCode, string? parentCode, string? type)
    {
        AreaId = areaId;
        ParentId = parentId;
        Name = name;
        Description = description;
        Code = code;
        FullCode = fullCode;
        ParentCode = parentCode;
        Type = type;
    }

    public OrganizationUnit Update(Guid? parentId, Guid? areaId, string name, string? description, string? code, string? fullCode, string? parentCode, string? type)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (code is not null && Code?.Equals(code) is not true) Code = code;
        if (fullCode is not null && FullCode?.Equals(fullCode) is not true) FullCode = fullCode;
        if (parentCode is not null && ParentCode?.Equals(parentCode) is not true) ParentCode = parentCode;

        if (type is not null && Type?.Equals(type) is not true) Type = type;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (parentId.HasValue && parentId.Value != Guid.Empty && !ParentId.Equals(parentId.Value)) ParentId = parentId.Value;
        if (areaId.HasValue && areaId.Value != Guid.Empty && !AreaId.Equals(areaId.Value)) AreaId = areaId.Value;

        return this;
    }
}