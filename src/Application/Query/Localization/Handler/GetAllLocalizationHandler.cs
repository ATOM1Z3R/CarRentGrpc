using Application.Dtos.Read;
using Domain.Interfaces;
using MediatR;

namespace Application.Query.Localization.Handler;

public class GetAllLocalizationHandler : IRequestHandler<GetAllLocalization, List<ReadLocalizationDto>>
{
    private readonly IUnitOfWork _uow;

    public GetAllLocalizationHandler(IUnitOfWork uow)
    => _uow = uow;

    public async Task<List<ReadLocalizationDto>> Handle(GetAllLocalization request, CancellationToken cancellationToken)
    {
        return await Task.Run(() => _uow.Localizations.GetAll()
            .Select(x => new ReadLocalizationDto(x))
            .ToList());
    }
}
