using Infrastructure.EfCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;
using tests.IntergrationTests.SeedData;

namespace tests.IntergrationTests;

public class BaseIntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:16.0")
        .WithDatabase("car_rent_db_test1")
        .WithUsername("test")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services => {
            services.RemoveAll(typeof(DbContextOptions<DataBaseContext>));

            services.AddDbContext<DataBaseContext>(options => {
                options.UseNpgsql(_dbContainer.GetConnectionString());
            });
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        var scope = Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<DataBaseContext>();

        context.Database.EnsureCreated();

        context.Localizations.AddRange(LocalizationSeed.Localizations);
        
        context.Cars.AddRange(CarSeed.Cars);

        context.Customers.AddRange(CustomerSeed.Customers);

        context.Reservations.AddRange(ReservationSeed.Reservations);

        await context.SaveChangesAsync();
    }

    public async new Task DisposeAsync()
    => await _dbContainer.DisposeAsync();
}
