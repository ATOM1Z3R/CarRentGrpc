using Domain.Models;

namespace tests.UnitTests;

public class LocalizationRepositoryTests : BaseUnitTest
{
    [Fact]
    public void GetAll_ShouldReturnLocalizations()
    {
        var expected = _context.Localizations.Count();

        var actual = uow.Localizations?
            .GetAll()?
            .Count();
        
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("city3")]
    [InlineData("city22")]
    public void GetByCity_ShouldReturnLocalizationsMatchingCity(string city)
    {
        var expected = _context.Localizations.Where(x => x.City == city);

        var actual = uow.Localizations.GetByCity(city);

        Assert.Equal(expected?.Count(), actual?.Count());
    }

    [Theory]
    [InlineData(32)]
    [InlineData(34)]
    public async Task GetByIdAsync_ShouldReturnLocalizationWithGivenId(int id)
    {
        var expected = await _context.Localizations.FindAsync(id);

        var actual = await uow.Localizations.GetByIdAsync(id);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull()
    {
        var actual = await uow.Localizations.GetByIdAsync(-1);

        Assert.Null(actual);
    }

    [Fact]
    public async Task AddAsync_ShouldCreateLocalization()
    {
        var newLocalization = new Localization {
            City = "City 17",
            Street = "Street 11"
        };
        await uow.Localizations.AddAsync(newLocalization);
    
        var changes = await uow.SaveAsync();
    
        Assert.True(changes > 0);
    }

    [Fact]
    public async Task AddAsync_ShouldFailToCreateLocalization()
    {
        var expected = "'Street' value must be provided";
        var actual = "";
        try
        {
            var newLocalization = new Localization {
                City = "City 17",
            };
            await uow.Localizations.AddAsync(newLocalization);
    
            await uow.SaveAsync();
        }
        catch (Exception ex)
        {
            actual = ex.Message;
        }
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(99999, 4567457)]
    [InlineData(99, 456635)]
    public async Task AddCar_ShouldAddCarToLocalization(int localizationId, int carId)
    {
        var localization = _context.Localizations.First(x => x.Id == localizationId);

        var car = _context.Cars.First(x => x.Id == carId);

        uow.Localizations.AddCar(localization, car);

        var changes = await uow.SaveAsync();
    
        Assert.True(changes > 0);
    }

    [Theory]
    [InlineData(5647)]
    public async Task  RemoveCar_ShouldRemoveCarFromLocalization(int carId)
    {
        var car = _context.Cars.First(x => x.Id == carId);

        uow.Localizations.RemoveCar(car);

        var changes = await uow.SaveAsync();
    
        Assert.True(changes > 0);
    }
}
