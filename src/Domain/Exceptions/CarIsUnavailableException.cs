namespace Domain.Exceptions;

public class CarIsUnavailableException : Exception
{
    public CarIsUnavailableException(string numberPlate) : base($"Car with numberplate: {numberPlate} is unavailable")
    { }
}
