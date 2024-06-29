using Application.Dtos.Common;
using Domain.Models;

namespace Application.Dtos.Read;

public class ReadCustomerDto : CustomerDto
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public ReadCustomerDto(Customer customer)
    {
        Id = customer.Id;
        FirstName = customer.FirstName;
        LastName = customer.LastName;
        PhoneNumber = customer.PhoneNumber;
        City = customer.City;
        Email = customer.Email;
        Street = customer.Street;
    }

    public static implicit operator ReadCustomerDto?(Customer customer)
    => customer is null ? null : new ReadCustomerDto(customer);
}
