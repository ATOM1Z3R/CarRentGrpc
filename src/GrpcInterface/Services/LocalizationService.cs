using Application.Command.Localization;
using Application.Dtos.Write;
using Application.Query.Localization;
using Domain.Enums;
using Google.Protobuf.WellKnownTypes;
using Grpc;
using Grpc.Core;
using GrpcInterface.Helpers;
using GrpcInterface.MessagesMappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace GrpcInterface.Services;

[Authorize]
public class LocalizationService : Localization.LocalizationBase
{
    private readonly IMediator _mediator;

    public LocalizationService(IMediator mediator)
    => _mediator = mediator;

    public async override Task<GetAllLocalizationsResponse> GetAllLocalizations(Empty request, ServerCallContext context)
    {
        var result = await _mediator.Send(new GetAllLocalization());
        var response = new GetAllLocalizationsResponse();
        response.Localizations.AddRange(
            result.Select(localization => new GetAllLocalizationsResponse.Types.Localization
            {
                Id = localization.Id,
                Localization_ = new LocalizationCommon
                {
                    Street = localization.Street,
                    City = localization.City
                },
            })
        );
        return response;
    }

    public async override Task<GetLocalizationsByCityResponse> GetLocalizationsByCity(GetLocalizationsByCityRequest request, ServerCallContext context)
    {
        var result = await _mediator.Send(new GetByCity(request.City));
        var response = new GetLocalizationsByCityResponse();
        response.Localizations.AddRange(result.Select(
            LocalizationServiceMappers.ReadRealizationDetailDtoToLocalization
        ));
        return response;
    }

    public async override Task<Empty> AddCarToLocalization(AddCarToLocalizationRequest request, ServerCallContext context)
    {
        CheckRoleHelper.Check(context.GetHttpContext(), EmployeeType.Renting);

        await _mediator.Send(new AddCarToLocalization(request.LocalizationId, request.CarId));
        return new Empty();

    }

    public async override Task<Empty> AddLocalization(AddLocalizationRequest request, ServerCallContext context)
    {
        CheckRoleHelper.Check(context.GetHttpContext(), EmployeeType.Renting);
        var localization = new WriteLocalizationDto()
        {
            City = request.Localization.City,
            Street = request.Localization.Street,
        };
        await _mediator.Send(new AddLocalization(localization));
        return new Empty();
    }

    public override async Task GetLocalizations(
        IAsyncStreamReader<GetLocalizationsRequest> requestStream,
        IServerStreamWriter<GetLocalizationsResponse> responseStream,
        ServerCallContext context)
    {
        while (await requestStream.MoveNext() && !context.CancellationToken.IsCancellationRequested)
        {
            var start = requestStream.Current.Start;
            var end = requestStream.Current.End;
            var response = await _mediator.Send(new GetLocalizations(start, end));
            var message = new GetLocalizationsResponse();
            message.Localizations.AddRange(
                response.Select(x => LocalizationServiceMappers.ReadRealizationDetailDtoToLocalization(x))
            );

            await responseStream.WriteAsync(message);
        }
    }
}
