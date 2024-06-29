using Application.Dtos.Read;
using Domain.Enums;
using MediatR;

namespace Application.Query.Reservation;

public record GetUretrievedReservations(ReservationStatusType status) : IRequest<List<ReadReservationDto>>;
