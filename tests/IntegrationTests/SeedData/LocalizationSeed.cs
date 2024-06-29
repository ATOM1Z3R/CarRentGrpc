using Domain.Models;

namespace tests.IntergrationTests.SeedData;

public static class LocalizationSeed
{
    public static List<Localization> Localizations { get; set; } = new()
    {
        new Localization { Id = 9999, City = "city1", Street = "street1" },
        new Localization { Id = 999, City = "city2", Street = "street2" },
        new Localization { Id = 99999, City = "city3", Street = "street3" },
        new Localization { Id = 99, City = "city3", Street = "street4" },
    };
}
