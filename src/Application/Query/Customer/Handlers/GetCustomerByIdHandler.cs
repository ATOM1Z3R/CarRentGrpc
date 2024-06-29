using Application.Dtos.Read;
using Domain.Interfaces;
using MediatR;

namespace Application.Query.Customer.Handlers;

public class GetCustomerByIdHandler : IRequestHandler<GetCustomerById, ReadCustomerDetailDto?>
{
    private readonly IUnitOfWork _uow;

    public GetCustomerByIdHandler(IUnitOfWork uow)
    => _uow = uow;

    public async Task<ReadCustomerDetailDto?> Handle(GetCustomerById request, CancellationToken cancellationToken)
    => await _uow.Customers.GetByIdAsync(request.id);
}
