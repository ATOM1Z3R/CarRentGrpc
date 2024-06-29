using Application.Dtos.Read;
using Domain.Interfaces;
using MediatR;

namespace Application.Query.Car.Handlers;

public class FindByNumberPlateHandler : IRequestHandler<FindByNumberPlate, List<ReadCarDto>>
{
    private readonly IUnitOfWork _uow;

    public FindByNumberPlateHandler(IUnitOfWork uow)
    => _uow = uow;

    public async Task<List<ReadCarDto>> Handle(FindByNumberPlate request, CancellationToken cancellationToken)
    {
        var cars = _uow.Cars.FindByNumberPlate(request.numberPlate);

        return cars?.Select(x => new ReadCarDto(x))
            .ToList() ?? new List<ReadCarDto>();
    }
}
