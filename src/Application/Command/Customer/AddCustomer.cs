using Application.Dtos.Write;
using MediatR;

namespace Application.Command.Customer;

public record AddCustomer(WriteCustomerDto Customer) : IRequest;
