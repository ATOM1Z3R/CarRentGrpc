namespace Domain.Exceptions;

public class EmployeeWithGivenIdDoesntExistException : Exception
{
    public EmployeeWithGivenIdDoesntExistException(int id) : base($"Employee with id {id} doesnt exist")
    { }
}
