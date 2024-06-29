using Application.Dtos.Read;
using MediatR;

namespace Application.Query.Customer;

public record GetCustomerById(int id) : IRequest<ReadCustomerDetailDto>;
