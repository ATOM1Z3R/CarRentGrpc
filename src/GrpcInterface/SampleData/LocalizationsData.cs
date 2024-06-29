using Domain.Models;

namespace GrpcInterface.SampleData;

public class LocalizationsData
{
    public static List<Localization> Localizations { get; set; } = new()
    {
        new Localization { Id = 1, City = "Jaworzno", Street = "Miedziana 7" },
        new Localization { Id = 2, City = "Sosnowiec", Street = "Kwaitowa 6" },
        new Localization { Id = 3, City = "Katowice", Street = "Zielona 44" },
    };
}