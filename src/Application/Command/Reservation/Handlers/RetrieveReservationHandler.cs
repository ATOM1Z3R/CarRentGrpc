using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Command.Reservation.Handlers;

public class RetrieveReservationHandler : IRequestHandler<RetrieveReservation>
{
    private readonly IUnitOfWork _uow;

    public RetrieveReservationHandler(IUnitOfWork uow)
    => _uow = uow;

    public async Task Handle(RetrieveReservation request, CancellationToken cancellationToken)
    {
        var reservationToRetrieve = await _uow.Reservations.GetByIdAsync(request.reservationId);

        if (reservationToRetrieve is null)
        {
            throw new ReservationNotFoundException(request.reservationId);
        }

        if (reservationToRetrieve.ActualRetrieveDate > DateTime.MinValue)
        {
            throw new ReservationAlreadyRetrievedException(request.reservationId);
        }

        var car = await _uow.Cars.GetByIdAsync(reservationToRetrieve.CarId);
        if (car is null)
        {
            throw new CarDoesntExistException(reservationToRetrieve.CarId);
        }

        car.Availability = true;

        _uow.Cars.Update(car);

        _uow.Reservations.Confirm(reservationToRetrieve);

        await _uow.SaveAsync();
    }
}
