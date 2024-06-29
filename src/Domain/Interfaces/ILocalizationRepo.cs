using Domain.Models;

namespace Domain.Interfaces;

public interface ILocalizationRepo : ICommmonRepo<Localization>
{
    IQueryable<Localization>? GetAll();

    IQueryable<Localization>? GetByCity(string city);

    IQueryable<Localization>? GetCollection(int start, int end);

    void AddCar(Localization localization, Car car);

    void RemoveCar(Car car);
}
