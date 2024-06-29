using Application.Command.Car;
using Application.Command.Customer;
using Application.Command.Localization;
using Application.Command.Reservation;
using Application.Dtos.Update;
using Application.Dtos.Write;
using Domain.Exceptions;
namespace tests.IntergrationTests;

public class CommandsTests : BaseIntegrationTest
{
    public CommandsTests(BaseIntegrationTestWebAppFactory factory) : base(factory)
    { }

    [Fact]
    public async Task AddCar_ShouldNotThrowAnyExceptions()
    {
        var newCar = new WriteCarDto {
            NumberOfSeats = 4,
            NumberPlate = "poiu1234",
            Model = "model99",
            Manufacturer = "man",
            Color = "white",
            Year = 2005,
            Availability = true,
            PriceMultiplier = 15,
        };
        var command = new AddCar(newCar);
        var exception = await Record.ExceptionAsync(() => Sender.Send(command));

        Assert.Null(exception);
    }

    [Fact]
    public async Task AddCustomer_ShouldNotThrowAnyExceptions()
    {
        var newCustomer = new WriteCustomerDto {
            FirstName = "firstname",
            LastName = "lastName",
            PhoneNumber = "123456789",
            Email = "test@test.com",
            City = "city",
            Street = "street 12",
        };
        var command = new AddCustomer(newCustomer);
        var exception = await Record.ExceptionAsync(() => Sender.Send(command));

        Assert.Null(exception);
    }

    [Fact]
    public async Task UpdateCustomer_ShouldNotThrowAnyExceptions()
    {
        var customer = DbContext.Customers.First();
        var updatedCustomer = new UpdateCustomerDto {
            Id = customer.Id,
            PhoneNumber = "123456786",
            Email = customer.Email,
            City = "cityy",
            Street = "street 122",
        };
        var command = new UpdateCustomer(updatedCustomer);
        var exception = await Record.ExceptionAsync(() => Sender.Send(command));

        Assert.Null(exception);
    }

    [Theory]
    [InlineData(456635, 56473)]
    public async Task MakeReservation_ShouldNotThrowAnyExceptions(int carId, int customerId)
    {
        var newReservation = new WriteReservationDto {
            RentDate = DateTime.MinValue,
            ExpectingRetrieveDate = DateTime.MinValue,
            CarId = carId,
            CustomerId = customerId,
        };
        var command = new MakeReservation(newReservation);
        var exception = await Record.ExceptionAsync(() => Sender.Send(command));

        Assert.Null(exception);
    }

    [Theory]
    [InlineData(4567457, 0456)]
    public async Task MakeReservation_ShouldThrowCustomerHaveActiveReservationException(int carId, int customerId)
    {
        var newReservation = new WriteReservationDto {
            RentDate = DateTime.MinValue,
            ExpectingRetrieveDate = DateTime.MinValue,
            CarId = carId,
            CustomerId = customerId,
        };
        var command = new MakeReservation(newReservation);
        var exception = await Record.ExceptionAsync(() => Sender.Send(command));

        Assert.IsType<CustomerHaveActiveReservationException>(exception);
    }

    [Theory]
    [InlineData(4567457, -1)]
    public async Task MakeReservation_ShouldThrowCustomerDoesntExistException(int carId, int customerId)
    {
        var newReservation = new WriteReservationDto {
            RentDate = DateTime.MinValue,
            ExpectingRetrieveDate = DateTime.MinValue,
            CarId = carId,
            CustomerId = customerId,
        };
        var command = new MakeReservation(newReservation);
        var exception = await Record.ExceptionAsync(() => Sender.Send(command));

        Assert.IsType<CustomerDoesntExistException>(exception);
    }

    [Theory]
    [InlineData(8)]
    public async Task RetrieveReservation_ShouldNotThrowAnyExceptions(int reservationId)
    {
        var command = new RetrieveReservation(reservationId);
        var exception = await Record.ExceptionAsync(() => Sender.Send(command));

        Assert.Null(exception);
    }

    [Theory]
    [InlineData(-1)]
    public async Task RetrieveReservation_ShouldThrowReservationNotFoundException(int reservationId)
    {
        var command = new RetrieveReservation(reservationId);
        var exception = await Record.ExceptionAsync(() => Sender.Send(command));

        Assert.IsType<ReservationNotFoundException>(exception);
    }

    [Theory]
    [InlineData(9999, 5647)]
    public async Task AddCarToLocalization_ShouldNotThrowAnyExceptions(int localizationId, int carId)
    {
        var command = new AddCarToLocalization(localizationId, carId);
        var exception = await Record.ExceptionAsync(() => Sender.Send(command));

        Assert.Null(exception);
    }

    [Theory]
    [InlineData(9999, -1)]
    public async Task AddCarToLocalization_ShouldThrowCarDoesntExistException(int localizationId, int carId)
    {
        var command = new AddCarToLocalization(localizationId, carId);
        var exception = await Record.ExceptionAsync(() => Sender.Send(command));

        Assert.IsType<CarDoesntExistException>(exception);
    }

    [Fact]
    public async Task AddLocalization_ShouldNotThrowAnyExceptions()
    {
        var newLocalization = new WriteLocalizationDto {
            City = "city1",
            Street = "street 55"
        };
        var command = new AddLocalization(newLocalization);
        var exception = await Record.ExceptionAsync(() => Sender.Send(command));

        Assert.Null(exception);
    }
}
