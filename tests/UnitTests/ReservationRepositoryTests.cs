using Domain.Models;

namespace tests.UnitTests;

public class ReservationRepositoryTests : BaseUnitTest
{
    [Theory]
    [InlineData(4567457, 56472)]
    public async Task AddAsync_ShouldCreateReservation(int carId, int customerId)
    {
        var car = _context.Cars.First(x => x.Id == carId);

        var customer = _context.Customers.First(x => x.Id == customerId);
        var newReservation = new Reservation {
            RentDate = new DateTime(2023, 05, 09),
            ExpectingRetrieveDate = new DateTime(2023, 05, 16),
            CarId = car.Id,
            CustomerId = customer.Id
        };
        await uow.Reservations.AddAsync(newReservation);
    
        var changes = await uow.SaveAsync();
    
        Assert.True(changes > 0);
    }

    [Theory]
    [InlineData("ExpectingRetrieveDate is earlier than RentDate", 456635, 56472)]
    public async Task AddAsync_ShouldThrowExpectingEarlierThanRentDateError(string expectedError, int carId, int customerId)
    {
        var actualError = "";
        try
        {
            var car = _context.Cars.First(x => x.Id == carId);

            var customer = _context.Customers.First(x => x.Id == customerId);
            var newReservation = new Reservation {
                RentDate = new DateTime(2023, 05, 09),
                ExpectingRetrieveDate = new DateTime(2023, 02, 16),
                Car = car,
                CarId = car.Id,
                Customer = customer,
                CustomerId = customer.Id
            };
            await uow.Reservations.AddAsync(newReservation);
        
            await uow.SaveAsync();
        }
        catch (Exception ex)
        {
            actualError = ex.Message;
        }
        Assert.Equal(expectedError, actualError);
    }

    [Theory]
    [InlineData(8)]
    public async Task Confirm_ShouldConfimReservation(int reservationId)
    {
        var reservation = _context.Reservations.First(x => x.Id == reservationId);

        uow.Reservations.Confirm(reservation);
        
        var changes = await uow.SaveAsync();

        Assert.True(changes > 0);
    }

    [Fact]
    public void GetAllUnretrieved_ShouldReturnUnretrievedReservations()
    {
        var expected = _context.Reservations
            .Where(x => DateTime.Compare(x.ActualRetrieveDate, DateTime.MinValue) == 0);

        var actual = uow.Reservations.GetUnretrieved();

        Assert.Equal(expected.Count(), actual.Count());
        Assert.NotNull(actual.First().Car);
        Assert.NotNull(actual.First().Customer);
    }

    [Theory]
    [InlineData(0456)]
    public void GetByCustomerId_ShouldReturnCustomerReservations(int customerId)
    {
        var expected = _context.Reservations.Where(x => x.CustomerId == customerId);

        var actual = uow.Reservations.GetByCustomerId(customerId);

        Assert.Equal(expected.Count(), actual.Count());
        Assert.NotNull(actual.First().Car);
        Assert.NotNull(actual.First().Customer);
    }

    [Fact]
    public void GetByDateRange_ShouldReturnReservations()
    {
        var actual = uow.Reservations.GetByDateRange(DateTime.MinValue, DateTime.MaxValue);

        Assert.True(actual.Count() > 0);
        Assert.NotNull(actual.First().Car);
        Assert.NotNull(actual.First().Customer);
    }

    [Fact]
    public void GetByDateRange_ShouldReturnEmptyCollection()
    {
        var actual = uow.Reservations.GetByDateRange(DateTime.MinValue, DateTime.MinValue);

        Assert.True(actual.Count() == 0);
    }

    [Theory]
    [InlineData(8)]
    [InlineData(88)]
    [InlineData(888)]
    public async Task GetByIdAsync_ShouldReturnReservation(int reservationId)
    {
        var expected = _context.Reservations.First(x => x.Id == reservationId);

        var actual = await uow.Reservations.GetByIdAsync(reservationId);

        Assert.Equal(expected, actual);
    }
}
