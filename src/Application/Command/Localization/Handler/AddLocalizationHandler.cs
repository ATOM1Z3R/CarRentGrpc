using Domain.Interfaces;
using MediatR;

namespace Application.Command.Localization.Handler;

public class AddLocalizationHandler : IRequestHandler<AddLocalization, Unit>
{
    private readonly IUnitOfWork _uow;

    public AddLocalizationHandler(IUnitOfWork uow)
    => _uow = uow;

    public async Task<Unit> Handle(AddLocalization request, CancellationToken cancellationToken)
    {
        await _uow.Localizations.AddAsync(request.localization);

        await _uow.SaveAsync();
        return Unit.Value;
    }
}
