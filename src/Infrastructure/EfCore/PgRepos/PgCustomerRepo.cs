using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Validators;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfCore.PgRepos;

public sealed class PgCustomerRepo : PgCommonRepo<Customer>, ICustomerRepo
{
    private readonly DbSet<Customer> _customers;

    public PgCustomerRepo(DataBaseContext db) : base(db)
    => _customers = db.Customers;

    // public override async Task AddAsync(Customer entity)
    // {
    //     BusinessModelValidator.Validate(entity, DataValidation.All);

    //     entity.BirthDate = entity.BirthDate.ToUniversalTime();

    //     entity.Created = entity.Created.ToUniversalTime();

    //     entity.Deleted = entity.Deleted.ToUniversalTime();

    //     await _customers.AddAsync(entity);
    // }

    public void Update(Customer updatedCustomer)
    {
        BusinessModelValidator.Validate(updatedCustomer, DataValidation.All);

        _customers.Update(updatedCustomer);
    }

    public async Task<bool> IsPhoneNumberInUseAsync(string phoneNumber)
    => await _customers.AnyAsync(x => x.Deleted == DateTime.MinValue && x.PhoneNumber == phoneNumber);

    public async Task<bool> IsEmailInUseAsync(string email)
    => await _customers.AnyAsync(x => x.Deleted == DateTime.MinValue && x.Email == email);

    public async Task<bool> IsExistAsymc(int id)
    => await _customers.AnyAsync(x => x.Deleted == DateTime.MinValue && x.Id == id);
}
