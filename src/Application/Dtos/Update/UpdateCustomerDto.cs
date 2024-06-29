using Application.Dtos.Common;
using Domain.Models;

namespace Application.Dtos.Update;

public class UpdateCustomerDto : CustomerDto
{
    public int Id { get; set; }

    public Customer AsCustomer()
    {
        return new Customer {
            Id = Id,
            PhoneNumber = PhoneNumber,
            Email = Email,
            City = City,
            Street = Street,
        };
    }

    public static implicit operator Customer?(UpdateCustomerDto customer) 
    => customer is null ? null : customer.AsCustomer();
}
