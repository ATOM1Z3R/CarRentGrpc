using Domain.Models;

namespace Domain.Interfaces;

public interface IReservationRepo : ICommmonRepo<Reservation>
{
    void Confirm(Reservation reservation);

    IQueryable<Reservation>? GetUnretrieved();

    IQueryable<Reservation>? GetUnretrieved(Func<Reservation, bool> filter);

    IQueryable<Reservation>? GetByCustomerId(int id);

    IQueryable<Reservation>? GetByDateRange(DateTime start, DateTime end);

    Task<bool> HaveCustomerActiveReservationAsync(int customerId);
}
