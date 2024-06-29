using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Caching.Memory;
using Infrastructure.EfCore.PgRepos;

namespace Infrastructure.Caching
{
    public sealed class CacheLocalizationRepo : ILocalizationRepo
    {
        private readonly PgLocalizationRepo _localizationRepo;

        private readonly IMemoryCache _cache;

        public CacheLocalizationRepo(PgLocalizationRepo localizationRepo, IMemoryCache cache)
        {
            _localizationRepo = localizationRepo;
            _cache = cache;
        }

        public async Task AddAsync(Localization entity)
        => await _localizationRepo.AddAsync(entity);

        public void AddCar(Localization localization, Car car)
        {
            _localizationRepo.AddCar(localization, car);
            _cache.Remove($"{nameof(Localization)}:{localization.Id}");
        }

        public IQueryable<Localization>? GetAll()
        => _localizationRepo.GetAll();

        public IQueryable<Localization>? GetByCity(string city)
        => _localizationRepo.GetByCity(city);

        public async Task<Localization?> GetByIdAsync(int id)
        {
            var cacheKey = $"{nameof(Localization)}:{id}";

            var localization = _cache.Get<Localization>(cacheKey);

            if (localization is not null)
            {
                return localization;
            }

            localization = await _localizationRepo.GetByIdAsync(id);

            _cache.Set<Localization>(cacheKey, localization);
            return localization;
        }

        public IQueryable<Localization>? GetCollection(int start, int end)
        => _localizationRepo.GetCollection(start, end);

        public void RemoveCar(Car car)
        {
            _localizationRepo.RemoveCar(car);
            _cache.Remove($"{nameof(Localization)}:{car.LocalizationId}");
        }
    }
}