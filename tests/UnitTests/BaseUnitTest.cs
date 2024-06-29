using Domain.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Infrastructure.EfCore;
using Infrastructure.EfCore.PgRepos;
using tests.UnitTests.SeedData;

namespace tests.UnitTests;

public class BaseUnitTest : IDisposable
{
    protected readonly DataBaseContext _context;
    protected IUnitOfWork uow;
    private readonly SqliteConnection _connection;

    public BaseUnitTest()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseSqlite(_connection)
                .Options;
        _context = new DataBaseContext(options);
        
        _context.Database.EnsureCreated();

        _context.Localizations.AddRange(LocalizationSeed.Localizations);

        _context.Cars.AddRange(CarSeed.Cars);

        _context.Customers.AddRange(CustomerSeed.Customers);

        _context.Reservations.AddRange(ReservationSeed.Reservations);

        _context.SaveChanges();

        uow = new UnitOfWork(
            _context,
            new PgCarRepo(_context),
            new PgLocalizationRepo(_context),
            new PgReservationRepo(_context),
            new PgCustomerRepo(_context),
            new PgEmployeeRepo(_context)
        );
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();

        _context.Dispose();

        _connection.Close();
    }
}
