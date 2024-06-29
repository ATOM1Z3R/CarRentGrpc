using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Command.Localization.Handler;

public class AddCarToLocalizationHandler : IRequestHandler<AddCarToLocalization>
{
    private readonly IUnitOfWork _uow;

    public AddCarToLocalizationHandler(IUnitOfWork uow)
    => _uow = uow;

    public async Task Handle(AddCarToLocalization request, CancellationToken cancellationToken)
    {
        var localization = await _uow.Localizations.GetByIdAsync(request.LocalizationId);
        if (localization is null)
        {
            throw new LocalizationDoesntExistException(request.LocalizationId);
        }
        var car = await _uow.Cars.GetByIdAsync(request.CarId);
        if (car is null)
        {
            throw new CarDoesntExistException(request.CarId);
        }
        if (localization.Cars.Any(x => x.Id == car.Id))
        {
            throw new CarAlreadyInLocalizationException(car.NumberPlate);
        }
        _uow.Localizations.AddCar(localization, car);
        
        await _uow.SaveAsync();
    }
}
