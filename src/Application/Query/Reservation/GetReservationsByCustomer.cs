using Application.Dtos.Read;
using MediatR;

namespace Application.Query.Reservation;

public record GetReservationsByCustomer(int customerId) : IRequest<List<ReadReservationDto>>;