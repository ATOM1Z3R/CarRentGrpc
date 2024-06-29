using Domain.Models;

namespace tests.UnitTests;

public class CustomerRepositoryTests : BaseUnitTest
{
    [Fact]
    public async Task AddAsync_ShouldCreateCustomer()
    {
        var newCustomer = new Customer {
            FirstName = "newName",
            LastName = "NewLastName",
            PhoneNumber = "123214234",
            Email = "email@new.cc",
            City = "cityCity",
            Street = "Street 66"
        };
        await uow.Customers.AddAsync(newCustomer);
    
        var changes = await uow.SaveAsync();
    
        Assert.True(changes > 0);
    }

    [Theory]
    [InlineData("Email is not valid")]
    public async Task AddAsync_ShouldThrowEmailVaildationError(string expectedError)
    {
        var actualError = "";
        try
        {
            var newCustomer = new Customer {
                FirstName = "newName",
                LastName = "NewLastName",
                PhoneNumber = "123214234",
                Email = "emailnew.cc",
                City = "cityCity",
                Street = "Street 66"
            };
            await uow.Customers.AddAsync(newCustomer);
        
            await uow.SaveAsync();
        }
        catch (Exception ex)
        {
            actualError = ex.Message;
        }
        Assert.Equal(expectedError, actualError);
    }

    [Theory]
    [InlineData(0456)]
    [InlineData(3451)]
    public async Task GetByIdAsync_ShouldReturnCustomer(int customerId)
    {
        var expected = _context.Customers.First(x => x.Id == customerId);

        var actual = await uow.Customers.GetByIdAsync(customerId);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(0456)]
    public async Task Update_ShouldUpdateCustomer(int customerId)
    {
        var customer = _context.Customers.First(x => x.Id == customerId);
        customer.City = "updated city";
        customer.FirstName = "updated first name";
        customer.LastName = "updated last name";
        customer.PhoneNumber = "543234567";

        uow.Customers.Update(customer);

        var changes = await uow.SaveAsync();

        var updated = _context.Customers.Find(customer.Id);

        Assert.Equal(customer, updated);

        Assert.True(changes > 0);
    }

    [Theory]
    [InlineData(0456)]
    public async Task IsPhoneNumberInUseAsync_ShouldReturnTrue(int customerId)
    {
        var customer = _context.Customers.First(x => x.Id == customerId);

        var actual = await uow.Customers.IsPhoneNumberInUseAsync(customer.PhoneNumber);

        Assert.True(actual);
    }

    [Theory]
    [InlineData(0456)]
    public async Task IsEmailInUseAsync_ShouldReturnTrue(int customerId)
    {
        var customer = _context.Customers.First(x => x.Id == customerId);

        var actual = await uow.Customers.IsEmailInUseAsync(customer.Email);

        Assert.True(actual);
    }
}
