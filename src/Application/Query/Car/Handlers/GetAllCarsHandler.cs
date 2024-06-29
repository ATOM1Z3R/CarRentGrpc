using Application.Dtos.Read;
using Domain.Enums;
using Domain.Interfaces;
using MediatR;

namespace Application.Query.Car.Handlers;

public class GetAllCarsHandler : IRequestHandler<GetAllCars, List<ReadCarDto>>
{
    private readonly IUnitOfWork _uow;

    public GetAllCarsHandler(IUnitOfWork uow)
    => _uow = uow;

    public async Task<List<ReadCarDto>> Handle(GetAllCars request, CancellationToken cancellationToken)
    {
        var cars = _uow.Cars.GetAll();
        switch (request.availability)
        {
            case CarAvailability.Available:
                cars = _uow.Cars.GetAll(x => x.Availability);
                break;
            case CarAvailability.Unavailable:
                cars = _uow.Cars.GetAll(x => !x.Availability);
                break;
            default:
                break;
        }
        return await Task.Run(() => cars?.Select(x => new ReadCarDto(x))
                .ToList() ?? new List<ReadCarDto>());
    }
}
