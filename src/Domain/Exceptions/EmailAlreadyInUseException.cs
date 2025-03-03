namespace Domain.Exceptions;

public class EmailAlreadyInUseException : Exception
{
    public EmailAlreadyInUseException(string email) : base($"Email {email} already in use")
    { }
}
