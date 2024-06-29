namespace Domain.Exceptions;

public class CustomerDoesntExistException : Exception
{
    public CustomerDoesntExistException(int id) : base($"Customer with id: {id} not exist")
    { }
}
