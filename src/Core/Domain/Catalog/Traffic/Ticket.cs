namespace TD.CitizenAPI.Domain.Catalog;

public class Ticket : AuditableEntity, IAggregateRoot
{
    public Guid? TripId { get; set; }
    public Guid? PassengerId { get; set; }
    public string? Seat { get; set; }
    public DateTime? Date { get; set; }
    public virtual Trip? Trip { get; set; }
    public virtual Passenger? Passenger { get; set; }

    public Ticket(Guid? tripId, Guid? passengerId, string? seat, DateTime? date)
    {
        TripId = tripId;
        PassengerId = passengerId;
        Seat = seat;
        Date = date;
    }

    public Ticket Update(Guid? tripId, Guid? passengerId, string? seat, DateTime? date)
    {
        if (tripId.HasValue && tripId.Value != Guid.Empty && !TripId.Equals(tripId.Value)) TripId = tripId.Value;
        if (passengerId.HasValue && passengerId.Value != Guid.Empty && !PassengerId.Equals(passengerId.Value)) PassengerId = passengerId.Value;
        if (seat is not null && Seat?.Equals(seat) is not true) Seat = seat;
        if (date.HasValue && !Date.Equals(date.Value))
        {
            Date = date.Value;
        }
        return this;
    }
}