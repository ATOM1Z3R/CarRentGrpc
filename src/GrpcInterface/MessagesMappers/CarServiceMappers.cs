using Application.Dtos.Read;
using Application.Dtos.Write;
using Grpc;

namespace GrpcInterface.MessagesMappers;

public static class CarServiceMappers
{
    public static WriteCarDto AddCarRequestToWriteCarDto(AddCarRequest request)
    {
        return new WriteCarDto
        {
            NumberOfSeats = request.Car.NumberOfSeats,
            NumberPlate = request.Car.NumberPlate,
            Model = request.Car.Model,
            Manufacturer = request.Car.Manufacturer,
            Color = request.Car.Color,
            Year = request.Car.Year,
            Availability = request.Car.Availability,
            PriceMultiplier = request.Car.PriceMultiplier
        };
    }

    public static CarResponseCommon ReadCarDtoToCarResponseCommon(ReadCarDto car)
    {
        return new CarResponseCommon
        {
            Id = car.Id,
            Car = new CarCommon
            {
                NumberPlate = car.NumberPlate,
                Manufacturer = car.Manufacturer,
                Model = car.Model,
                Color = car.Color,
                NumberOfSeats = car.NumberOfSeats,
                Year = car.Year,
                Availability = car.Availability,
                PriceMultiplier = car.PriceMultiplier,
            },
            Localization = car.Localization is null
                ? null : new CarResponseCommon.Types.CarLocalization
                {
                    Id = car.Localization.Id,
                    Street = car.Localization.Street,
                    City = car.Localization.City
                }
        };
    }
}
