namespace TD.CitizenAPI.Domain.Catalog;

//Huong Dan su dung
public class HuongDanSuDung : AuditableEntity, IAggregateRoot
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? File { get; set; }

    public HuongDanSuDung(string name,  string? file, string? description)
    {
        Name = name;
        Description = description;
        File = file;
    }

    public HuongDanSuDung Update(string? name, string? file, string? description)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (file is not null && File?.Equals(file) is not true) File = file;
        return this;
    }
}