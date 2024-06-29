using Application.Dtos.Read;
using MediatR;

namespace Application.Query.Localization;

public record GetByCity(string city) : IRequest<List<ReadLocalizationDetailDto>>;
