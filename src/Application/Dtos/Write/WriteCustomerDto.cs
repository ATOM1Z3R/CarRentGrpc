using Application.Dtos.Common;
using Domain.Models;

namespace Application.Dtos.Write;

public sealed class WriteCustomerDto : CustomerDto
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public Customer AsCustomer()
    {
        return new Customer() {
            FirstName = FirstName,
            LastName = LastName,
            PhoneNumber = PhoneNumber,
            Email = Email,
            City = City,
            Street = Street
        };
    }

    public static implicit operator Customer(WriteCustomerDto customerDto)
    => customerDto.AsCustomer();
}
