namespace TD.CitizenAPI.Domain.Catalog;

public class Passenger : AuditableEntity, IAggregateRoot
{
    public string Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }

    public Passenger(string name, string? email, string? phone)
    {
        Name = name;
        Email = email;
        Phone = phone;
    }

    public Passenger Update(string name, string? email, string? phone)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (email is not null && Email?.Equals(email) is not true) Email = email;
        if (Phone is not null && Phone?.Equals(phone) is not true) Phone = phone;

        return this;
    }
}