using Application.Dtos.Read;
using MediatR;

namespace Application.Query.Reservation;

public record GetReservationsByDate(DateTime start, DateTime end) : IRequest<List<ReadReservationDto>>;
