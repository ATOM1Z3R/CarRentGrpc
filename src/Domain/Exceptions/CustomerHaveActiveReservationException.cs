namespace Domain.Exceptions;

public class CustomerHaveActiveReservationException : Exception
{
    public CustomerHaveActiveReservationException(int customerId) : base($"Customer with id: {customerId} have active reservation")
    { }
}