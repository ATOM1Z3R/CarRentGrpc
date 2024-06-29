using Application.Dtos.Read;
using Domain.Interfaces;
using MediatR;

namespace Application.Query.Localization.Handler;

public class GetByCityHandler : IRequestHandler<GetByCity, List<ReadLocalizationDetailDto>>
{
    private readonly IUnitOfWork _uow;

    public GetByCityHandler(IUnitOfWork uow)
    => _uow = uow;

    public async Task<List<ReadLocalizationDetailDto>> Handle(GetByCity request, CancellationToken cancellationToken)
    {
        var localizations = _uow.Localizations.GetByCity(request.city)?
            .Select(x => new ReadLocalizationDetailDto(x));

        return await Task.Run(() => localizations?.ToList() ?? new List<ReadLocalizationDetailDto>());
    }
}
