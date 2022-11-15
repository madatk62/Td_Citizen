namespace TD.CitizenAPI.Application.Catalog.Tickets;

public class TicketDto : IDto
{
    public Guid Id { get; set; }
    public Guid? TripId { get; set; }
    public Guid? PassengerId { get; set; }
    public string? Seat { get; set; }
    public DateTime? Date { get; set; }
    public virtual Trip? Trip { get; set; }
    public virtual Passenger? Passenger { get; set; }
}