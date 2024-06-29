namespace Domain.Exceptions;

public class CarAlreadyInLocalizationException : Exception
{
    public CarAlreadyInLocalizationException(string numberPlate) : base($"Car with number plate {numberPlate} is already in localization")
    { }
}
