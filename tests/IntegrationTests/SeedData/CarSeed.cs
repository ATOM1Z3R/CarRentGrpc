using Domain.Models;

namespace tests.IntergrationTests.SeedData;

public static class CarSeed
{
    public static List<Car> Cars = new()
    {
        new Car {
            Id = 9078786,
            Manufacturer = "man1",
            Model = "mod1",
            Color = "Kolor",
            Availability = false,
            Year = 1996,
            NumberOfSeats = 2,
            NumberPlate = "yyy1",
            Localization = LocalizationSeed.Localizations[0],
            LocalizationId = LocalizationSeed.Localizations[0].Id,
        },
        new Car {
            Id = 456635,
            Manufacturer = "man2",
            Model = "mod2",
            Color = "Kolor",
            Availability = true,
            Year = 1996,
            NumberOfSeats = 2,
            NumberPlate = "yyy4",
            Localization = LocalizationSeed.Localizations[0],
            LocalizationId = LocalizationSeed.Localizations[0].Id,
        },
        new Car {
            Id = 4567457,
            Manufacturer = "man3",
            Model = "mod3",
            Color = "Kolor",
            Availability = true,
            Year = 1996,
            NumberOfSeats = 2,
            NumberPlate = "xxx3",
            Localization = LocalizationSeed.Localizations[0],
            LocalizationId = LocalizationSeed.Localizations[0].Id,
        },
        new Car {
            Id = 5647,
            Manufacturer = "man4",
            Model = "mod4",
            Color = "Kolor",
            Availability = false,
            NumberOfSeats = 2,
            Year = 1996,
            NumberPlate = "xxx4",
            Localization = LocalizationSeed.Localizations[1],
            LocalizationId = LocalizationSeed.Localizations[1].Id,
        },
    };
}
