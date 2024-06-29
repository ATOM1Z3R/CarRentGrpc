using Domain.Models;

namespace tests.UnitTests;

public class CarRepositoryTests : BaseUnitTest
{
    [Fact]
    public async Task AddAsync_ShouldCreateCar()
    {
        var car = new Car {
            Manufacturer = "mewCar",
            Model = "model1",
            Year = 2003,
            Color = "White",
            PriceMultiplier = 5,
            NumberOfSeats = 4,
            NumberPlate = "qqq1111",
            Availability = true,
        };

        await uow.Cars.AddAsync(car);

        var changes = await uow.SaveAsync();

        Assert.True(changes > 0);
    }

    [Fact]
    public async Task AddAsync_ShouldThrowManufactureFieldError()
    {
        var expected = "'Manufacturer' value must be provided";
        var actual = "";
        try
        {
            var car = new Car {
                Model = "model1",
                Year = 2003,
                Color = "White",
                PriceMultiplier = 5,
                NumberOfSeats = 4,
                NumberPlate = "qqq1111",
                Availability = true,
            };

            await uow.Cars.AddAsync(car);

            await uow.SaveAsync();
        }
        catch (Exception ex)
        {
            actual = ex.Message;
        }
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("'NumberPlate' value must be provided")]
    public async Task AddAsync_ShouldThrowNumberPlateFieldError(string expectedError)
    {
        var actualError = "";
        try
        {
            var car = new Car {
                Manufacturer = "mewCar",
                Model = "model1",
                Year = 2003,
                Color = "White",
                PriceMultiplier = 5,
                NumberOfSeats = 4,
                Availability = true,
            };

            await uow.Cars.AddAsync(car);

            await uow.SaveAsync();
        }
        catch (Exception ex)
        {
            actualError = ex.Message;
        }
        Assert.Equal(expectedError, actualError);
    }

    [Theory]
    [InlineData("xxx")]
    [InlineData("yyy")]
    public void FindByNumberPlate_ShouldReturnCollection(string numberPlate)
    {
        var expected = _context.Cars
            .Where(x => x.NumberPlate.Contains(numberPlate))
            .Count();

        var actual = uow.Cars?
            .FindByNumberPlate(numberPlate)?
            .Count();

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(11)]
    [InlineData(12)]
    [InlineData(-1)]
    public async Task GetByIdAsync_ShouldReturnCar(int carId)
    {
        var expected = _context.Cars.FirstOrDefault(x => x.Id == carId);

        var actual = await uow.Cars.GetByIdAsync(carId);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task Update_ShouldUpdateCar()
    {
        var car = _context.Cars.First();
        car.Model = "Updated model 111";
        uow.Cars.Update(car);

        var changes = await uow.SaveAsync();

        var updated = _context.Cars.First(x => x.Id == car.Id);

        Assert.True(changes > 0);
        Assert.Equal(car, updated);
    }

    [Fact]
    public void GetAll_ShouldReturnCollection()
    {
        var expected = _context.Cars.Count();

        var actual = uow.Cars?
            .GetAll()?
            .Count();

        Assert.Equal(expected, actual);
    }
}
