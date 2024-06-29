using Application.Command.Customer;
using Application.Query.Customer;
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
public class CustomerService : Custmer.CustmerBase
{
    private readonly IMediator _mediator;

    public CustomerService(IMediator mediator)
    => _mediator = mediator;

    public async override Task<Empty> AddCustomer(AddCustomerRequest request, ServerCallContext context)
    {
        CheckRoleHelper.Check(context.GetHttpContext(), EmployeeType.Renting);

        var newCustomer = CustomerServiceMappers.AddCustomerRequestToWriteCustomerDto(request);

        await _mediator.Send(new AddCustomer(newCustomer));
        return new Empty();
    }

    public async override Task<GetCustomerByIdResponse> GetById(GetCustomerByIdRequest request, ServerCallContext context)
    {
        var customer = await _mediator.Send(new GetCustomerById(request.Id));

        return CustomerServiceMappers.ReadCustomerDtoToGetCustomerByIdResponse(customer);
    }

    public async override Task<Empty> Update(UpdateCustomerRequest request, ServerCallContext context)
    {
        CheckRoleHelper.Check(context.GetHttpContext(), EmployeeType.Renting);

        var updatedCustomer = CustomerServiceMappers.UpdateCustomerRequestToUpdateCustomerDto(request);

        await _mediator.Send(new UpdateCustomer(updatedCustomer));
        return new Empty();
    }
}
