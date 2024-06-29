using Application.Dtos.Write;
using MediatR;

namespace Application.Command.Reservation;

public record MakeReservation(WriteReservationDto reservation) : IRequest;
