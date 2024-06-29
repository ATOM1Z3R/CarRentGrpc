using Domain.Models;

namespace tests.UnitTests.SeedData;

public static class ReservationSeed
{
    public static List<Reservation> Reservations = new()
    {
        new Reservation { 
            Id = 8,
            RentDate = new DateTime(2023, 10, 20).ToUniversalTime(),
            ExpectingRetrieveDate = new DateTime(2023, 10, 26).ToUniversalTime(),
            ActualRetrieveDate = DateTime.MinValue.ToUniversalTime(),
            Car = CarSeed.Cars[0],
            CarId = CarSeed.Cars[0].Id,
            Customer = CustomerSeed.Customers[1],
            CustomerId = CustomerSeed.Customers[1].Id,
        },
        new Reservation {
            Id = 88,
            RentDate = new DateTime(2023, 07, 20).ToUniversalTime(),
            ExpectingRetrieveDate = new DateTime(2023, 07, 23).ToUniversalTime(),
            ActualRetrieveDate = new DateTime(2023, 07, 23).ToUniversalTime(),
            Car = CarSeed.Cars[1],
            CarId = CarSeed.Cars[1].Id,
            Customer = CustomerSeed.Customers[0],
            CustomerId = CustomerSeed.Customers[0].Id,
        },
        new Reservation {
            Id = 888,
            RentDate = new DateTime(2023, 10, 20).ToUniversalTime(),
            ExpectingRetrieveDate = new DateTime(2099, 08, 25).ToUniversalTime(),
            ActualRetrieveDate = DateTime.MinValue.ToUniversalTime(),
            Car = CarSeed.Cars[1],
            CarId = CarSeed.Cars[1].Id,
            Customer = CustomerSeed.Customers[0],
            CustomerId = CustomerSeed.Customers[0].Id,
        },
    };
}
