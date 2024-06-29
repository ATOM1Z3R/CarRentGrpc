using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfCore.PgRepos;

public sealed class PgEmployeeRepo : IEmployeeRepo
{
    private readonly DbSet<Employee> _employees;

    public PgEmployeeRepo(DataBaseContext db)
    => _employees = db.Employees;

    public async Task<Employee?> GetByEmailAsync(string email)
    => await _employees
        .FirstOrDefaultAsync(x => x.Deleted == DateTime.MinValue && x.Email.ToUpper() == email.ToUpper());

    public async Task<Employee?> GetByIdAsync(int id)
    => await _employees.FirstOrDefaultAsync(x => x.Deleted == DateTime.MinValue && x.Id == id);

    public async Task AddEmp(Employee employee)
    => await _employees.AddAsync(employee);
}
