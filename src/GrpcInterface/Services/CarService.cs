using Application.Command.Car;
using Application.Query.Car;
using Domain.Enums;
using Google.Protobuf.WellKnownTypes;
using Grpc;
using Grpc.Core;
using GrpcInterface.Helpers;
using GrpcInterface.MessagesMappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace GrpcInterface.Services;

[Authorize]
public class CarService : Car.CarBase
{
    private readonly IMediator _mediator;

    public CarService(IMediator mediator)
    => _mediator = mediator;

    public async override Task<Empty> AddCar(AddCarRequest request, ServerCallContext context)
    {
        CheckRoleHelper.Check(context.GetHttpContext(), EmployeeType.Renting);

        var newCar = CarServiceMappers.AddCarRequestToWriteCarDto(request);

        await _mediator.Send(new AddCar(newCar));
        return new Empty();
    }

    public override async Task<FindCarByNumberPlateResponse> FindCarByNumberPlate(FindCarByNumberPlateRequest request, ServerCallContext context)
    {
        var cars = await _mediator.Send(new FindByNumberPlate(request.NumberPlate));
        var response = new FindCarByNumberPlateResponse();
        response.Cars.AddRange(cars.Select(x =>
            CarServiceMappers.ReadCarDtoToCarResponseCommon(x)
        ));
        return response;
    }

    public override async Task<GetAllCarsResponse> GetAllCars(GetAllCarsRequest request, ServerCallContext context)
    {
        var cars = await _mediator.Send(new GetAllCars((Domain.Enums.CarAvailability)request.Availability));
        var response = new GetAllCarsResponse();
        response.Cars.AddRange(
            cars.Select(x => CarServiceMappers.ReadCarDtoToCarResponseCommon(x))
        );
        return response;
    }
}
