using Domain.Models;

namespace Application.Dtos.Read;

public class ReadReservationDto
{
    public int Id { get; set; }

    public DateTime RentDate { get; set; }

    public DateTime ExpectingRetrieveDate { get; set; }

    public DateTime ActualRetrieveDate { get; set; }

    public ReadCarDto Car { get; set; }

    public ReadCustomerDto Customer { get; set; }

    public ReadReservationDto(Reservation reservation, DateTimeKind dateTimeKind)
    {
        Id = reservation.Id;
        RentDate = DateTime.SpecifyKind(reservation.RentDate, dateTimeKind);
        ExpectingRetrieveDate = DateTime.SpecifyKind(reservation.ExpectingRetrieveDate, dateTimeKind);
        ActualRetrieveDate = DateTime.SpecifyKind(reservation.ActualRetrieveDate, dateTimeKind);
        Customer = reservation.Customer;
        Car = reservation.Car;
    }
}
