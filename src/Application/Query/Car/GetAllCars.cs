using Application.Dtos.Read;
using Domain.Enums;
using MediatR;

namespace Application.Query.Car;

public record GetAllCars(CarAvailability availability) : IRequest<List<ReadCarDto>>;
