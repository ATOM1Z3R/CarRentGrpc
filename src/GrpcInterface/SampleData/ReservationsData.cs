using Domain.Models;

namespace GrpcInterface.SampleData;

public class ReservationsData
{
    public static List<Reservation> Reservations { get; set; } = new()
    {
        new Reservation { 
            Id = 9,
            RentDate = new DateTime(2023, 10, 20).ToUniversalTime(),
            ExpectingRetrieveDate = new DateTime(2023, 10, 26).ToUniversalTime(),
            ActualRetrieveDate = DateTime.MinValue.ToUniversalTime(),
            Car = CarsData.Cars[0],
            CarId = CarsData.Cars[0].Id,
            Customer = CustomersData.Customers[1],
            CustomerId = CustomersData.Customers[1].Id,
        },
        new Reservation {
            Id = 10,
            RentDate = new DateTime(2023, 07, 20).ToUniversalTime(),
            ExpectingRetrieveDate = new DateTime(2023, 07, 23).ToUniversalTime(),
            ActualRetrieveDate = new DateTime(2023, 07, 23).ToUniversalTime(),
            Car = CarsData.Cars[1],
            CarId = CarsData.Cars[1].Id,
            Customer = CustomersData.Customers[0],
            CustomerId = CustomersData.Customers[0].Id,
        },
        new Reservation {
            Id = 11,
            RentDate = new DateTime(2023, 10, 20).ToUniversalTime(),
            ExpectingRetrieveDate = new DateTime(2099, 08, 25).ToUniversalTime(),
            ActualRetrieveDate = DateTime.MinValue.ToUniversalTime(),
            Car = CarsData.Cars[1],
            CarId = CarsData.Cars[1].Id,
            Customer = CustomersData.Customers[0],
            CustomerId = CustomersData.Customers[0].Id,
        }
    };
}