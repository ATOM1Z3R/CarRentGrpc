using Domain.Models;

namespace Application.Dtos.Write;

public sealed class WriteReservationDto
{
    public DateTime RentDate { get; set; }

    public DateTime ExpectingRetrieveDate { get; set; }

    public int CarId { get; set; }

    public int CustomerId { get; set; }

    private Reservation AsReservation()
    => new Reservation() {
        RentDate = RentDate,
        ExpectingRetrieveDate = ExpectingRetrieveDate,
        CarId = CarId,
        CustomerId = CustomerId
    };

    public static implicit operator Reservation(WriteReservationDto reservationDto)
    => reservationDto.AsReservation();
}
