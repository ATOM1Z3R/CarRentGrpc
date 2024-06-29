using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Command.Customer.Handlers;

public class UpdateCustomerHandler : IRequestHandler<UpdateCustomer>
{
    private readonly IUnitOfWork _uow;

    public UpdateCustomerHandler(IUnitOfWork uow)
    => _uow = uow;
    
    public async Task Handle(UpdateCustomer request, CancellationToken cancellationToken)
    {
        var customer = await _uow.Customers.GetByIdAsync(request.updateCustomer.Id);
        if (customer is null)
        {
            throw new CustomerDoesntExistException(request.updateCustomer.Id);
        }
        if (request.updateCustomer.Email != customer.Email
        && await _uow.Customers.IsEmailInUseAsync(request.updateCustomer.Email))
        {
            throw new EmailAlreadyInUseException(request.updateCustomer.Email);
        }
        if (request.updateCustomer.PhoneNumber != customer.PhoneNumber
        && await _uow.Customers.IsPhoneNumberInUseAsync(request.updateCustomer.PhoneNumber))
        {
            throw new PhoneNumberAlreadyInUseException(request.updateCustomer.PhoneNumber);
        }
        customer.City = request.updateCustomer.City;
        customer.Street = request.updateCustomer.Street;
        customer.Email = request.updateCustomer.Email;
        customer.PhoneNumber = request.updateCustomer.PhoneNumber;
        _uow.Customers.Update(customer);

        await _uow.SaveAsync();
    }
}
