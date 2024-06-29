using Domain.Models;

namespace Domain.Interfaces;

public interface ICarRepo : ICommmonRepo<Car>
{
    IQueryable<Car>? FindByNumberPlate(string numberPlate);

    IQueryable<Car>? GetAll();

    IQueryable<Car>? GetAll(Func<Car, bool> filter);

    Task<bool> IsNumberPlateInUseAsync(string numberPlate);

    void Update(Car car);
}
