using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Command.Reservation.Handlers;

public class MakeReservationHandler : IRequestHandler<MakeReservation>
{
    private readonly IUnitOfWork _uow;

    public MakeReservationHandler(IUnitOfWork uow)
    => _uow = uow;
    
    public async Task Handle(MakeReservation request, CancellationToken cancellationToken)
    {
        if (!await _uow.Customers.IsExistAsymc(request.reservation.CustomerId))
        {
            throw new CustomerDoesntExistException(request.reservation.CustomerId);
        }
        if (await _uow.Reservations.HaveCustomerActiveReservationAsync(request.reservation.CustomerId))
        {
            throw new CustomerHaveActiveReservationException(request.reservation.CustomerId);
        }
        var car = await _uow.Cars.GetByIdAsync(request.reservation.CarId);
        if (car is null)
        {
            throw new CarDoesntExistException(request.reservation.CarId);
        }
        if (!car.Availability)
        {
            throw new CarIsUnavailableException(car.NumberPlate);
        }
        car.Availability = false;
        _uow.Cars.Update(car);

        await _uow.Reservations.AddAsync(request.reservation);

        await _uow.SaveAsync();
    }
}
