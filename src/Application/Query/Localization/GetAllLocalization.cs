using Application.Dtos.Read;
using MediatR;

namespace Application.Query.Localization;

public record GetAllLocalization() : IRequest<List<ReadLocalizationDto>>;
