using Application.Dtos.Read;
using Domain.Enums;
using Domain.Interfaces;
using MediatR;

namespace Application.Query.Reservation.Handler;

public record GetUretrievedReservationsHandler : IRequestHandler<GetUretrievedReservations, List<ReadReservationDto>>
{
    private readonly IUnitOfWork _uow;

    public GetUretrievedReservationsHandler(IUnitOfWork uow)
    => _uow = uow;

    public async Task<List<ReadReservationDto>> Handle(GetUretrievedReservations request, CancellationToken cancellationToken)
    {
        IQueryable<Domain.Models.Reservation>? reservations = default;
        switch (request.status)
        {
            case ReservationStatusType.Normal:
                reservations = _uow.Reservations.GetUnretrieved(
                    x => x.ExpectingRetrieveDate >= DateTime.Now
                );
                break;
            case ReservationStatusType.Late:
                reservations = _uow.Reservations.GetUnretrieved(
                    x => x.ExpectingRetrieveDate <= DateTime.Now
                );
                break;
            default:
                reservations = _uow.Reservations.GetUnretrieved();
                break;
        }
        var reseravtionsDtos = reservations?.Select(x => new ReadReservationDto(x, DateTimeKind.Utc))
            .ToList() ?? new List<ReadReservationDto>();

        return await Task.Run(() => reseravtionsDtos);
    }
}

