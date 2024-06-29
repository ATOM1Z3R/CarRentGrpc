using Microsoft.Extensions.Caching.Memory;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.EfCore.PgRepos;

namespace Infrastructure.Caching;

public sealed class CacheCarRepo : ICarRepo
{
    private readonly PgCarRepo _carRepo;

    private readonly IMemoryCache _cache;

    public CacheCarRepo(PgCarRepo carRepo, IMemoryCache cache)
    {
        _carRepo = carRepo;
        _cache = cache;
    }

    public async Task AddAsync(Car entity)
    => await _carRepo.AddAsync(entity);

    public IQueryable<Car>? FindByNumberPlate(string numberPlate)
    => _carRepo.FindByNumberPlate(numberPlate);

    public IQueryable<Car>? GetAll()
    => _carRepo.GetAll();

    public IQueryable<Car>? GetAll(Func<Car ,bool> filter)
    => _carRepo.GetAll(filter);

    public async Task<Car?> GetByIdAsync(int id)
    {
        var cacheKey = $"{nameof(Car)}:{id}";

        var car = _cache.Get<Car>(cacheKey);

        if (car is not null)
        {
            return car;
        }

        car = await _carRepo.GetByIdAsync(id);

        _cache.Set<Car>(cacheKey, car);
        return car;
    }

    public async Task<bool> IsNumberPlateInUseAsync(string numberPlate)
    => await _carRepo.IsNumberPlateInUseAsync(numberPlate);

    public void Update(Car car)
    {
        _carRepo.Update(car);
        _cache.Remove($"{nameof(Car)}:{car.Id}");
    }
}
