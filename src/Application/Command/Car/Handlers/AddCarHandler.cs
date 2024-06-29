using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Command.Car.Handlers;

public class AddCarHandler : IRequestHandler<AddCar>
{
    private readonly IUnitOfWork _uow;

    public AddCarHandler(IUnitOfWork uow)
    => _uow = uow;

    public async Task Handle(AddCar request, CancellationToken cancellationToken)
    {
        if (await _uow.Cars.IsNumberPlateInUseAsync(request.car.NumberPlate))
        {
            throw new CarWithGivenNumberPlateAlreadyExistException(request.car.NumberPlate);
        }
        await _uow.Cars.AddAsync(request.car);
        
        await _uow.SaveAsync();
    }
}
