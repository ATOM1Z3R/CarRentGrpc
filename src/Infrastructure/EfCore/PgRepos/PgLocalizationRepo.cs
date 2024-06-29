using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfCore.PgRepos;

public sealed class PgLocalizationRepo : PgCommonRepo<Localization>, ILocalizationRepo
{
    private readonly DbSet<Localization> _localizations;

    private readonly DbSet<Car> _cars;

    public PgLocalizationRepo(DataBaseContext db) : base(db)
    {
        _localizations = db.Localizations;
        _cars = db.Cars;
    }

    public void AddCar(Localization localization, Car car)
    {
        car.LocalizationId = localization.Id;
        _cars.Update(car);
    }

    public IQueryable<Localization>? GetAll()
    => _localizations.Where(x => x.Deleted == DateTime.MinValue);

    public IQueryable<Localization>? GetByCity(string city)
    => _localizations
        .Where(x => x.Deleted == DateTime.MinValue && x.City.ToLower() == city.ToLower())?
        .Include(x => x.Cars.Where(x => x.Deleted == null));

    public IQueryable<Localization>? GetCollection(int start, int end)
    {
        return _localizations
            .Include(x => x.Cars)
            .Where(x => x.Deleted == DateTime.MinValue)
            .Skip(start)
            .Take(end);
    }

    public void RemoveCar(Car car)
    {
        car.LocalizationId = null;
        _cars.Update(car);
    }

    public override async Task<Localization?> GetByIdAsync(int id)
    => await _localizations
        .Include(x => x.Cars)
        .FirstOrDefaultAsync(x => x.Deleted == DateTime.MinValue && x.Id == id);
}
