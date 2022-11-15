namespace TD.CitizenAPI.Application.Catalog.Passengers;

public class PassengerDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Email { get; set; }
    public string? Phone { get; set; }
}