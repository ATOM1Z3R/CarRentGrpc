using Application.Dtos.Read;
using MediatR;

namespace Application.Query.Car;

public record FindByNumberPlate(string numberPlate) : IRequest<List<ReadCarDto>>;
