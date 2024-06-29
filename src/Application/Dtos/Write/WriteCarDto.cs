using Application.Dtos.Common;
using Domain.Models;

namespace Application.Dtos.Write;

public sealed class WriteCarDto : CarDto
{
    public Car AsCar()
    => new Car()
    {
        NumberOfSeats = NumberOfSeats,
        NumberPlate = NumberPlate,
        Model = Model,
        Manufacturer = Manufacturer,
        Color = Color,
        Year = Year,
        Availability = Availability,
        PriceMultiplier = PriceMultiplier
    };

    public static implicit operator Car(WriteCarDto carDto)
    => carDto.AsCar();
}
