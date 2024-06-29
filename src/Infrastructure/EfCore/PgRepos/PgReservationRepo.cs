using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Validators;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfCore.PgRepos;

public sealed class PgReservationRepo : PgCommonRepo<Reservation>, IReservationRepo
{
    private readonly DbSet<Reservation> _reservations;

    public PgReservationRepo(DataBaseContext db) : base(db)
    => _reservations = db.Reservations;

    public override async Task AddAsync(Reservation entity)
    {
        BusinessModelValidator.Validate(entity, DataValidation.All);

        entity.ExpectingRetrieveDate = entity.ExpectingRetrieveDate.ToUniversalTime();
        
        entity.RentDate = entity.RentDate;

        entity.ActualRetrieveDate = entity.ActualRetrieveDate.ToUniversalTime();

        entity.Created = entity.Created.ToUniversalTime();

        entity.Deleted = entity.Deleted.ToUniversalTime();

        await _reservations.AddAsync(entity);
    }

    public void Confirm(Reservation reservation)
    {
        reservation.ActualRetrieveDate = DateTime.UtcNow;
        _reservations.Update(reservation);
    }

    public IQueryable<Reservation>? GetUnretrieved()
    => _reservations
        .Include(x => x.Car)
        .Include(x => x.Customer)
        .Where(x => x.Deleted == DateTime.MinValue
            && x.ActualRetrieveDate == DateTime.MinValue);

    public IQueryable<Reservation>? GetUnretrieved(Func<Reservation, bool> filter)
    => GetUnretrieved()?
        .Where(filter)
        .AsQueryable();

    public IQueryable<Reservation>? GetByCustomerId(int id)
    => _reservations
        .Where(x => x.Deleted == DateTime.MinValue && x.CustomerId == id)?
        .Include(x => x.Car)
        .Include(x => x.Customer);

    public IQueryable<Reservation>? GetByDateRange(DateTime start, DateTime end)
    => _reservations
        .Where(x =>  x.Deleted == DateTime.MinValue && x.RentDate >= start && x.RentDate <= end)?
        .Include(x => x.Car)
        .Include(x => x.Customer);

    public async Task<bool> HaveCustomerActiveReservationAsync(int customerId)
    => await _reservations.AnyAsync(
        x => x.CustomerId == customerId && x.ActualRetrieveDate == DateTime.MinValue
    );
}
