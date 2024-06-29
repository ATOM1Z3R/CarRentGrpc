using Domain.Models;

namespace Domain.Interfaces;

public interface ICustomerRepo : ICommmonRepo<Customer>
{
    void Update(Customer updatedCustomer);

    Task<bool> IsExistAsymc(int id);

    Task<bool> IsPhoneNumberInUseAsync(string phoneNumber);

    Task<bool> IsEmailInUseAsync(string email);
}
