using MediatR;

namespace Application.Command.Localization;

public record AddCarToLocalization(int LocalizationId, int CarId) : IRequest;
    