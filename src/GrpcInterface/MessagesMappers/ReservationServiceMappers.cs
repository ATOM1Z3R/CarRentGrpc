using Application.Dtos.Read;
using Application.Dtos.Write;
using Google.Protobuf.WellKnownTypes;
using Grpc;

namespace GrpcInterface.MessagesExtensions;

public static class ReservationServiceMappers
{
    public static WriteReservationDto MakeRequestToWriteReservationDto(MakeRequest makeRequest)
    {
        return new WriteReservationDto()
            {
                RentDate = makeRequest.Reservation.RentDate.ToDateTime(),
                ExpectingRetrieveDate = makeRequest.Reservation.ExpectingRetrieveDate.ToDateTime(),
                CarId = makeRequest.CarId,
                CustomerId = makeRequest.CustomerId,
            };
    }
    public static GetReservationsResponse.Types.Reservation ReadReservationDtoToReservation(ReadReservationDto reservation)
    {
        return new GetReservationsResponse.Types.Reservation
        {
            Id = reservation.Id,
            ActualRetrieveDate = reservation.ActualRetrieveDate.ToTimestamp(),
            Reservation_ = new ReservationCommon
            {
                RentDate = reservation.RentDate.ToTimestamp(),
                ExpectingRetrieveDate = reservation.ExpectingRetrieveDate.ToTimestamp(),
            },
            Car = new GetReservationsResponse.Types.Reservation.Types.Car
            {
                Id = reservation.Car.Id,
                NumberPlate = reservation.Car.NumberPlate,
                Manufacturer = reservation.Car.Manufacturer,
                Model = reservation.Car.Model,
                Color = reservation.Car.Color,
                NumberOfSeats = reservation.Car.NumberOfSeats,
                Year = reservation.Car.Year,
                Availability = reservation.Car.Availability,
                PriceMultiplier = reservation.Car.PriceMultiplier,
            },
            Customer = new GetReservationsResponse.Types.Reservation.Types.Customer
            {
                Id = reservation.Customer.Id,
                FirstNamme = reservation.Customer.FirstName,
                LastName = reservation.Customer.LastName,
                Email = reservation.Customer.Email,
                PhoneNumber = reservation.Customer.PhoneNumber,
            }
        };
    }
}
