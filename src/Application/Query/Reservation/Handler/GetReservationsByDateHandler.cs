using Application.Dtos.Read;
using Domain.Interfaces;
using MediatR;

namespace Application.Query.Reservation.Handler;

public class GetReservationsByDateHandler : IRequestHandler<GetReservationsByDate, List<ReadReservationDto>>
{
    private readonly IUnitOfWork _uow;

    public GetReservationsByDateHandler(IUnitOfWork uow)
    => _uow = uow;
    
    public async Task<List<ReadReservationDto>> Handle(GetReservationsByDate request, CancellationToken cancellationToken)
    {
        var reservations = _uow.Reservations.GetByDateRange(request.start, request.end)?
            .Select(x => new ReadReservationDto(x, DateTimeKind.Utc))
            .ToList() ?? new List<ReadReservationDto>();

        return await Task.Run(() => reservations);
    }
}
