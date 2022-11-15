namespace TD.CitizenAPI.Domain.Catalog;

public class AppConfig : AuditableEntity, IAggregateRoot
{
    public string Key { get; set; }
    public string Value { get; set; }
    public string? Description { get; set; }

    public AppConfig(string key, string value, string? description)
    {
        Key = key;
        Value = value;
        Description = description;
    }

    public AppConfig Update(string? key, string? value, string? description)
    {
        if (key is not null && Key?.Equals(key) is not true) Key = key;
        if (value is not null && Value?.Equals(value) is not true) Value = value;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        return this;
    }
}