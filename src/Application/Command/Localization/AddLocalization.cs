using MediatR;
using Application.Dtos.Write;

namespace Application.Command.Localization;

public record AddLocalization(WriteLocalizationDto localization) : IRequest<Unit>;

