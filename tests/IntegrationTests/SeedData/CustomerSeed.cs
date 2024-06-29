using Domain.Models;

namespace tests.IntergrationTests.SeedData;

public static class CustomerSeed
{
    public static List<Customer> Customers = new()
    {
        new Customer { Id = 0456, FirstName = "Name", LastName = "LastName", Email = "ee@ee.pl", City = "City 17", Street = "street 55" },
        new Customer { Id = 3451, FirstName = "First", LastName = "Last", Email = "ee1@ee1.pl", City = "City 17", Street = "street 55" },
        new Customer { Id = 56472, FirstName = "FirstName", LastName = "Name", Email = "ee2@ee2.pl", City = "City 17", Street = "street 55" },
        new Customer { Id = 56473, FirstName = "FirstNameee", LastName = "Nameee", Email = "ee222@ee.pl", City = "City 17", Street = "street 55" },
    };
}
