using Application.Dtos.Read;
using Domain.Interfaces;
using MediatR;

namespace Application.Query.Reservation.Handler;

public class GetReservationsByCustomerHandler : IRequestHandler<GetReservationsByCustomer, List<ReadReservationDto>>
{
    private readonly IUnitOfWork _uow;

    public GetReservationsByCustomerHandler(IUnitOfWork uow)
    => _uow = uow;

    public async Task<List<ReadReservationDto>> Handle(GetReservationsByCustomer request, CancellationToken cancellationToken)
    {
        var reservations = _uow.Reservations.GetByCustomerId(request.customerId)?
            .Select(x => new ReadReservationDto(x, DateTimeKind.Utc))
            .ToList() ?? new List<ReadReservationDto>();

        return await Task.Run(() => reservations);
    }
}
