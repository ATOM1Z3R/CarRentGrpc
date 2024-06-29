using Application.Dtos.Read;
using Grpc;

namespace GrpcInterface.MessagesMappers;

public static class LocalizationServiceMappers
{
    public static GetLocalization ReadRealizationDetailDtoToLocalization(
        ReadLocalizationDetailDto localization)
    {
        var localizationResponse = new GetLocalization
        {
            Id = localization.Id,
            Localization = new LocalizationCommon
            {
                Street = localization.Street,
                City = localization.City
            },
        };
        var cars = localization.Cars.Select(car => new GetLocalization.Types.Car
        {
            Id = car.Id,
            NumberPlate = car.NumberPlate,
            Manufacturer = car.Manufacturer,
            Model = car.Model,
            Color = car.Color,
            NumberOfSeats = car.NumberOfSeats,
            Year = car.Year,
            Availability = car.Availability,
            PriceMultiplier = car.PriceMultiplier,
        });
        localizationResponse.Cars.AddRange(cars);
        return localizationResponse;
    }
}
