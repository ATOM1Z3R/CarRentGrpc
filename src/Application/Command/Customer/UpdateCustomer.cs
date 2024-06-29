using Application.Dtos.Update;
using MediatR;

namespace Application.Command.Customer;

public record UpdateCustomer(UpdateCustomerDto updateCustomer) : IRequest;

