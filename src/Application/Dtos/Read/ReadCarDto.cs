using Application.Dtos.Common;
using Domain.Models;

namespace Application.Dtos.Read;

public sealed class ReadCarDto : CarDto
{
    public int Id { get; set; }

    public ReadLocalizationDto? Localization { get; set; }

    public ReadCarDto(Car car)
    {
        Id = car.Id;
        NumberPlate = car.NumberPlate;
        Manufacturer = car.Manufacturer;
        Model = car.Model;
        Color = car.Color;
        NumberOfSeats = car.NumberOfSeats;
        Year = car.Year;
        Availability = car.Availability;
        PriceMultiplier = car.PriceMultiplier;
        Localization = car.Localization;
    }

    public static implicit operator ReadCarDto?(Car car)
    => car is null ? null : new ReadCarDto(car);
}
