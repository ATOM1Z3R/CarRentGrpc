namespace Domain.Exceptions;

public class CarDoesntExistException : Exception
{
    public CarDoesntExistException(int id) : base($"Car with id: {id} doesnt exist")
    { }
}
