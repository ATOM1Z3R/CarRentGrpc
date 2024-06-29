namespace Domain.Exceptions;

public class PhoneNumberAlreadyInUseException : Exception
{
    public PhoneNumberAlreadyInUseException(string phoneNumber) : base($"Phone {phoneNumber} already in use")
    { }
}