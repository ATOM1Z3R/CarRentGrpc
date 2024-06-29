using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfCore.PgRepos;

public sealed class PgCarRepo : PgCommonRepo<Car>, ICarRepo
{
    private readonly DbSet<Car> _cars;

    public PgCarRepo(DataBaseContext db) : base(db)
    => _cars = db.Cars;

    public IQueryable<Car>? FindByNumberPlate(string numberPlate)
    => _cars.Where(x => x.Deleted == DateTime.MinValue && x.NumberPlate.Contains(numberPlate));

    public IQueryable<Car>? GetAll()
    => _cars.Where(X => X.Deleted == DateTime.MinValue);

    public IQueryable<Car>? GetAll(Func<Car, bool> filter)
    => GetAll()?
        .Where(filter)?
        .AsQueryable();

    public async Task<bool> IsNumberPlateInUseAsync(string numberPlate)
    => await _cars.AnyAsync(
        x => x.Deleted == DateTime.MinValue && x.NumberPlate.ToLower() == numberPlate.ToLower()
    );

    public void Update(Car car)
    => _cars.Update(car);
}
