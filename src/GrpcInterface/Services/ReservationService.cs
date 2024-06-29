using Application.Command.Reservation;
using Application.Query.Reservation;
using Domain.Enums;
using Google.Protobuf.WellKnownTypes;
using Grpc;
using Grpc.Core;
using GrpcInterface.Helpers;
using GrpcInterface.MessagesExtensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace GrpcInterface.Services;

[Authorize]
public class ReservationService : Reservation.ReservationBase
{
    private readonly IMediator _mediator;

    public ReservationService(IMediator mediator)
    => _mediator = mediator;

    public async override Task<GetReservationsResponse> GetByCustomer(GetByCustomerRequest request, ServerCallContext context)
    {
        var result = await _mediator.Send(new GetReservationsByCustomer(request.CustomerId));
        var response = new GetReservationsResponse();
        response.Reservations.AddRange(
            result.Select(res => ReservationServiceMappers.ReadReservationDtoToReservation(res))
        );
        return response;
    }

    public async override Task<GetReservationsResponse> GetByDate(GetByDateRequest request, ServerCallContext context)
    {
        var getReservationsByDate = new GetReservationsByDate(request.StartDate.ToDateTime(), request.EndDate.ToDateTime());

        var result = await _mediator.Send(getReservationsByDate);
        var response = new GetReservationsResponse();
        response.Reservations.AddRange(
            result.Select(res => ReservationServiceMappers.ReadReservationDtoToReservation(res))
        );
        return response;
    }

    public async override Task<GetReservationsResponse> GetUretrieved(GetUretrievedRequest request, ServerCallContext context)
    {
        var result = await _mediator.Send(new GetUretrievedReservations((ReservationStatusType)request.Status));

        var response = new GetReservationsResponse();
        response.Reservations.AddRange(
            result.Select(res => ReservationServiceMappers.ReadReservationDtoToReservation(res))
        );
        return response;
    }

    public async override Task<Empty> Make(MakeRequest request, ServerCallContext context)
    {
        CheckRoleHelper.Check(context.GetHttpContext(), EmployeeType.Renting);

        var reservation = ReservationServiceMappers.MakeRequestToWriteReservationDto(request);

        await _mediator.Send(new MakeReservation(reservation));
        return new Empty();

    }

    public async override Task<Empty> Retrieve(RetrieveRequest request, ServerCallContext context)
    {
        CheckRoleHelper.Check(context.GetHttpContext(), EmployeeType.Renting);

        await _mediator.Send(new RetrieveReservation(request.ReservationId));
        return new Empty();

    }
}
