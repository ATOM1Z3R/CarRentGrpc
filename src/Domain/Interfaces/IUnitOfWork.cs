namespace Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    public ILocalizationRepo Localizations { get; }

    public ICarRepo Cars { get; }

    public IReservationRepo Reservations { get; }

    public ICustomerRepo Customers { get; }

    public IEmployeeRepo Employees { get; }

    Task<int> SaveAsync();
}
