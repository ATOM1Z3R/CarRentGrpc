namespace Domain.Interfaces;

public interface ICommmonRepo<T> where T : class
{
    Task<T?> GetByIdAsync(int id);

    Task AddAsync(T entity);
}
