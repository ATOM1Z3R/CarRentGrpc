namespace Domain.Exceptions;

public class ReservationNotFoundException : Exception
{
    public ReservationNotFoundException(int id) : base($"Reservation with id: {id} not exist")
    { }
}
