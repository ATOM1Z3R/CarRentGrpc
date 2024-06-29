using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Caching.Memory;
using Infrastructure.EfCore.PgRepos;

namespace Infrastructure.Caching;

public sealed class CacheReservationRepo : IReservationRepo
{
    private readonly PgReservationRepo _reservationRepo;

    private readonly IMemoryCache _cache;

    public CacheReservationRepo(PgReservationRepo reservationRepo, IMemoryCache cache)
    {
        _reservationRepo = reservationRepo;
        _cache = cache;
    }

    public async Task AddAsync(Reservation entity)
    => await _reservationRepo.AddAsync(entity);

    public void Confirm(Reservation reservation)
    => _reservationRepo.Confirm(reservation);

    public IQueryable<Reservation>? GetUnretrieved()
    => _reservationRepo.GetUnretrieved();

    public IQueryable<Reservation>? GetUnretrieved(Func<Reservation, bool> filter)
    => _reservationRepo.GetUnretrieved(filter);

    public IQueryable<Reservation>? GetByCustomerId(int id)
    => _reservationRepo.GetByCustomerId(id);

    public IQueryable<Reservation>? GetByDateRange(DateTime start, DateTime end)
    => _reservationRepo.GetByDateRange(start, end);

    public async Task<Reservation?> GetByIdAsync(int id)
    {
        var cacheKey = $"{nameof(Reservation)}:{id}";

        var reservation = _cache.Get<Reservation>(cacheKey);

        return reservation ?? await _reservationRepo.GetByIdAsync(id);
    }

    public async Task<bool> HaveCustomerActiveReservationAsync(int customerId)
    => await _reservationRepo.HaveCustomerActiveReservationAsync(customerId);
}
