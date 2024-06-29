using Application.Utils;
using Application.Utils.Params;
using Infrastructure.EfCore;

namespace GrpcInterface.SampleData;

public static class InitSampleData
{
    public static void InitData(DataBaseContext context)
    {
        if (context.Database.EnsureCreated())
        {
            AddSampleUsers(context);
            context.Cars.AddRange(CarsData.Cars);
            context.Customers.AddRange(CustomersData.Customers);
            context.Reservations.AddRange(ReservationsData.Reservations);
            context.Localizations.AddRange(LocalizationsData.Localizations);
            context.SaveChanges();
        }
    }

    public static void AddSampleUsers(DataBaseContext context)
    {
        var pass1 = PasswordUtils.Hash(
            new PasswordParams {
                UnhashedPassword = "test1234"
            }
        );

        var pass2 = PasswordUtils.Hash(
            new PasswordParams {
                UnhashedPassword = "test4321"
            }
        );

        context.Employees.AddRange(
            new Domain.Models.Employee {
                FirstName = "Andrzej",
                LastName = "Szybki",
                PhoneNumber = "333333333",
                Email = "andrzej.szybki@qq.pl",
                Role = Domain.Enums.EmployeeType.Marketing,
                Password = pass1.Item1,
                Salt = pass1.Item2,
            },
            new Domain.Models.Employee {
                FirstName = "Janusz",
                LastName = "Kowalski",
                PhoneNumber = "88888888",
                Email = "janusz.kowalski@ss.pl",
                Role = Domain.Enums.EmployeeType.Renting,
                Password = pass2.Item1,
                Salt = pass2.Item2,
            }
        );

        context.SaveChanges();
    }
}