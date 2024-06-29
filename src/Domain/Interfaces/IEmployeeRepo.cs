using Domain.Models;

namespace Domain.Interfaces;

public interface IEmployeeRepo
{
    Task<Employee?> GetByEmailAsync(string email);

    Task<Employee?> GetByIdAsync(int id);

    Task AddEmp(Employee employee);
}
