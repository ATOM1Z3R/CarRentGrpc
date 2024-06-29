using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Command.Customer.Handlers;

public class AddCustomerHandler : IRequestHandler<AddCustomer>
{
    private readonly IUnitOfWork _uow;

    public AddCustomerHandler(IUnitOfWork uow)
    => _uow = uow;

    public async Task Handle(AddCustomer request, CancellationToken cancellationToken)
    {
        if (await _uow.Customers.IsEmailInUseAsync(request.Customer.Email))
        {
            throw new EmailAlreadyInUseException(request.Customer.Email);
        }
        if (await _uow.Customers.IsPhoneNumberInUseAsync(request.Customer.PhoneNumber))
        {
            throw new PhoneNumberAlreadyInUseException(request.Customer.PhoneNumber);
        }
        await _uow.Customers.AddAsync(request.Customer);
        await _uow.SaveAsync();
    }
}
