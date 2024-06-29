namespace Domain.Exceptions;

public class ReservationAlreadyRetrievedException : Exception
{
    public ReservationAlreadyRetrievedException(int reservationId) : base($"Reservation with id: {reservationId} already retrieved")
    { }
}