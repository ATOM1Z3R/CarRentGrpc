using Application.Dtos.Read;
using MediatR;

namespace Application.Query.Localization;

public record GetLocalizations(int start, int end) : IRequest<List<ReadLocalizationDetailDto>>;
