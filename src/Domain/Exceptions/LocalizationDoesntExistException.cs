namespace Domain.Exceptions;

public class LocalizationDoesntExistException : Exception
{
    public LocalizationDoesntExistException(int id) : base($"Location with given id: {id} doesnt exist")
    { }
}
