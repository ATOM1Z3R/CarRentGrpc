using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Read;
using Domain.Interfaces;
using MediatR;

namespace Application.Query.Localization.Handler;

public class GetLocalizationsHandler : IRequestHandler<GetLocalizations, List<ReadLocalizationDetailDto>>
{
    private IUnitOfWork _uow;

    public GetLocalizationsHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<List<ReadLocalizationDetailDto>> Handle(GetLocalizations request, CancellationToken cancellationToken)
    {
        var localizations = _uow.Localizations
            .GetCollection(request.start, request.end)?
            .Select(x => new ReadLocalizationDetailDto(x));
        return await Task.Run(
            () => localizations?.ToList() ?? new List<ReadLocalizationDetailDto>()
        );
    }
}
