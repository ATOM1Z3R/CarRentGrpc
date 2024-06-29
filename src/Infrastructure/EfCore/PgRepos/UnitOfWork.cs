using Domain.Interfaces;

namespace Infrastructure.EfCore;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly DataBaseContext db;

    public ILocalizationRepo Localizations { get; }

    public ICarRepo Cars { get; }

    public IReservationRepo Reservations { get; }

    public ICustomerRepo Customers { get; }

    public IEmployeeRepo Employees { get; }

    public UnitOfWork(
        DataBaseContext dbContext,
        ICarRepo carRepo,
        ILocalizationRepo localizationRepo,
        IReservationRepo reservationRepo,
        ICustomerRepo customerRepo,
        IEmployeeRepo employeeRepo
    )
    {
        db = dbContext;
        Localizations = localizationRepo;
        Cars = carRepo;
        Reservations = reservationRepo;
        Customers = customerRepo;
        Employees = employeeRepo;
    }

    public async Task<int> SaveAsync()
    => await db.SaveChangesAsync();

    public void Dispose()
    {
        db.Dispose();
        GC.SuppressFinalize(this);
    }
}
