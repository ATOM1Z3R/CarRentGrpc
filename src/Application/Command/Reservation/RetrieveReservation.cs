using MediatR;

namespace Application.Command.Reservation;

public record RetrieveReservation(int reservationId) : IRequest;
