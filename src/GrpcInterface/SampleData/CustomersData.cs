using Domain.Models;

namespace GrpcInterface.SampleData;

public class CustomersData
{
    public static List<Customer> Customers = new()
    {
        new Customer { Id = 12, FirstName = "Andrzej", LastName = "Kowal", Email = "andrzej.kowal@ee.pl", City = "Jaworzno", Street = "Polna 1" },
        new Customer { Id = 13, FirstName = "Kalasanty", LastName = "Kurleto", Email = "k.kurleto@wtf.pl", City = "Sosnowiec", Street = "Kolejowa 8" },
        new Customer { Id = 14, FirstName = "Janusz", LastName = "Pas", Email = "j.pas@wtf.pl", City = "Katowice", Street = "Zielona 23" },
    };
}