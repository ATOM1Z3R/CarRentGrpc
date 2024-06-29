using Domain.Models;

namespace GrpcInterface.SampleData;

public class CarsData
{
    public static List<Car> Cars { get; set; } = new()
    {
        new Car {
            Id = 4,
            Manufacturer = "Opel",
            Model = "Corsa",
            Color = "Czarny",
            Availability = false,
            Year = 1996,
            NumberOfSeats = 4,
            NumberPlate = "FTY7654X",
            PriceMultiplier = 4,
            Localization = LocalizationsData.Localizations[0],
            LocalizationId = LocalizationsData.Localizations[0].Id,
        },
        new Car {
            Id = 5,
            Manufacturer = "Ford",
            Model = "Focus RS",
            Color = "Niebieski",
            Availability = true,
            Year = 2018,
            NumberOfSeats = 4,
            NumberPlate = "PPL23452",
            PriceMultiplier = 14,
            Localization = LocalizationsData.Localizations[0],
            LocalizationId = LocalizationsData.Localizations[0].Id,
        },
        new Car {
            Id = 6,
            Manufacturer = "BMW",
            Model = "M8",
            Color = "Srebrny",
            Availability = true,
            Year = 1996,
            NumberOfSeats = 4,
            NumberPlate = "AA334455",
            PriceMultiplier = 24,
            Localization = LocalizationsData.Localizations[1],
            LocalizationId = LocalizationsData.Localizations[1].Id,
        },
        new Car {
            Id = 7,
            Manufacturer = "BMW",
            Model = "Z4",
            Color = "Zielony",
            Availability = false,
            NumberOfSeats = 2,
            Year = 2006,
            NumberPlate = "ZXC234567",
            PriceMultiplier = 10,
            Localization = LocalizationsData.Localizations[2],
            LocalizationId = LocalizationsData.Localizations[2].Id,
        },
        new Car {
            Id = 8,
            Manufacturer = "Farrari",
            Model = "812 Superfast",
            Color = "Czerwony",
            Availability = false,
            NumberOfSeats = 2,
            Year = 2019,
            NumberPlate = "QAZ1233445",
            PriceMultiplier = 33,
            Localization = LocalizationsData.Localizations[2],
            LocalizationId = LocalizationsData.Localizations[2].Id,
        },
    };
}