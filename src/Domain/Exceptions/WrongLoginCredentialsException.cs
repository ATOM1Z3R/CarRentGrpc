namespace Domain.Exceptions;

public class WrongLoginCredentialsException : Exception
{
    public WrongLoginCredentialsException() : base("Wrong login or password")
    { }
}
