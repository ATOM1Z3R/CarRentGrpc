using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Infrastructure.EfCore.Configurations;

namespace Infrastructure.EfCore;

public sealed class DataBaseContext : DbContext
{
    public DbSet<Car> Cars { get; init; }

    public DbSet<Customer> Customers { get; init; }

    public DbSet<Reservation> Reservations { get; init; }

    public DbSet<Localization> Localizations { get; init; }

    public DbSet<Employee> Employees { get; init; }

    public DataBaseContext (DbContextOptions<DataBaseContext> options)
    : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration<Employee>(new EmployeeConfig());
        modelBuilder.ApplyConfiguration<Customer>(new CustomerConfig());
        modelBuilder.ApplyConfiguration<Car>(new CarConfig());
        modelBuilder.ApplyConfiguration<Localization>(new LocalizationConfig());
        modelBuilder.ApplyConfiguration<Reservation>(new ReservationConfig());

        base.OnModelCreating(modelBuilder);
    }
}
