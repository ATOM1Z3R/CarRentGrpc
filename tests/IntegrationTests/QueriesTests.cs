using Application.Dtos.Read;
using Application.Query.Auth;
using Application.Query.Car;
using Application.Query.Customer;
using Application.Query.Localization;
using Application.Query.Reservation;
using Application.Utils;
using Application.Utils.Params;
using Domain.Enums;
using Domain.Exceptions;

namespace tests.IntergrationTests;

public class QueriesTests : BaseIntegrationTest
{
    private const string TEST_KEY = "0bd0ocgertrdg0/GdhboIW37R6z8I0sdgemiPeG055SJxPPJ7dfBo2tjJ3bPVGtRfCRDuusj24hdfgrstgeru";

    public QueriesTests(BaseIntegrationTestWebAppFactory factory) : base(factory)
    { }

    [Theory]
    [InlineData("test1@test.com", "test1234")]
    public async Task Login_ShouldReturnJwtTokenDto(string email, string password)
    {
        var passParams = new PasswordParams {
            UnhashedPassword = password
        };
        var pass = PasswordUtils.Hash(passParams);

        await DbContext.Employees.AddAsync(new Domain.Models.Employee {
            FirstName = "Test",
            LastName = "TEstTest",
            PhoneNumber = "88888888",
            Email = email,
            Role = Domain.Enums.EmployeeType.Renting,
            Password = pass.Item1,
            Salt = pass.Item2,
        });

        await DbContext.SaveChangesAsync();
        var query = new Login(email, password, TEST_KEY);
        var result = await Sender.Send(query);

        Assert.IsType<JwtTokenDto>(result);
    }

    [Theory]
    [InlineData("test1@test.com", "wrongpass")]
    public async Task Login_ShouldReturnWrongLoginCredentialsException(string email, string password)
    {
        var query = new Login(email, password, TEST_KEY);
        var exception = await Record.ExceptionAsync(() => Sender.Send(query));

        Assert.IsType<WrongLoginCredentialsException>(exception);
    }

    [Theory]
    [InlineData("yyy1", 1)]
    [InlineData("xxx", 2)]
    public async Task FindByNumberPlate_ShouldReturnCarsCollectionMatchingNumberPlate(
        string numberPlateSearch, 
        int expectedCount)
    {
        var query = new FindByNumberPlate(numberPlateSearch);
        var result = await Sender.Send(query);

        Assert.IsType<List<ReadCarDto>>(result);

        Assert.Equal(expectedCount, result.Count);
    }

    [Theory]
    [InlineData(CarAvailability.All, 4)]
    [InlineData(CarAvailability.Available, 2)]
    [InlineData(CarAvailability.Unavailable, 2)]
    public async Task GetAllCars_ShouldReturnCarsCollectionWithGiventStatus(
        CarAvailability status,
        int expectedCount)
    {
        var query = new GetAllCars(status);
        var result = await Sender.Send(query);

        Assert.IsType<List<ReadCarDto>>(result);

        Assert.Equal(expectedCount, result.Count);
    }

    [Fact]
    public async Task GetAllLocalizations_ShouldReturnAllLocalizations()
    {
        var expectedCount = DbContext.Localizations.Count();
        var query = new GetAllLocalization();
        var result = await Sender.Send(query);
        var actualCount = result.Count;

        Assert.Equal(expectedCount, actualCount);
    }

    [Theory]
    [InlineData("city3", 2)]
    [InlineData("city1", 1)]
    public async Task GetByCity_ShouldReturnLocalizations(string city, int expectedCount)
    {
        var query = new GetByCity(city);
        var result = await Sender.Send(query);
        var actualCount = result.Count;

        Assert.Equal(expectedCount, actualCount);
    }

    [Theory]
    [InlineData(1, 2, 2)]
    [InlineData(10, 20, 0)]
    public async Task GetAllLocalizations_ShouldReturnLocalizations(int start, int end, int expectedCount)
    {
        var query = new GetLocalizations(start, end);
        var result = await Sender.Send(query);
        var actualCount = result.Count;

        Assert.Equal(expectedCount, actualCount);
    }

    [Theory]
    [InlineData(ReservationStatusType.Late, 1)]
    [InlineData(ReservationStatusType.Normal, 1)]
    public async Task GetUretrievedReservations_ShouldReturnUnretrievedReservations(ReservationStatusType status, int expectedCount)
    {
        var query = new GetUretrievedReservations(status);
        var result = await Sender.Send(query);
        var actualCount = result.Count;

        Assert.Equal(expectedCount, actualCount);
    }

    public static object[][] ReservationsDateRange { get; } = {
        new object[] { new DateTime(2023, 10, 10).ToUniversalTime(), new DateTime(2023, 10, 22).ToUniversalTime(), 2 },
        new object[] { new DateTime(2021, 10, 10).ToUniversalTime(), new DateTime(2021, 10, 22).ToUniversalTime(), 0 },
    };

    [Theory, MemberData(nameof(ReservationsDateRange))]
    public async Task GetReservationsByDate_ShouldReturnReservationsFromDateRange(DateTime start, DateTime end, int expectedCount)
    {
        var query = new GetReservationsByDate(start, end);
        var result = await Sender.Send(query);
        var actualCount = result.Count;

        Assert.Equal(expectedCount, actualCount);
    }

    [Theory]
    [InlineData(0456, 2)]
    [InlineData(-99999, 0)]
    public async Task GetReservationsByCustomer_ShouldReturnReservationsFromDateRange(int customerId, int expectedCount)
    {
        var query = new GetReservationsByCustomer(customerId);
        var result = await Sender.Send(query);
        var actualCount = result.Count;

        Assert.Equal(expectedCount, actualCount);
    }

    [Theory]
    [InlineData(0456)]
    [InlineData(3451)]
    public async Task GetCustomerById_ShouldReturnCustomer(int customerId)
    {
        var query = new GetCustomerById(customerId);
        var result = await Sender.Send(query);

        Assert.IsType<ReadCustomerDetailDto>(result);
    }

    [Theory]
    [InlineData(-999)]
    public async Task GetCustomerById_ShouldReturnNull(int customerId)
    {
        var query = new GetCustomerById(customerId);
        var result = await Sender.Send(query);

        Assert.Null(result);
    }
}
