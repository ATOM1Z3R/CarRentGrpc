using Application.Dtos.Write;
using MediatR;

namespace Application.Command.Car;

public record AddCar(WriteCarDto car) : IRequest;
