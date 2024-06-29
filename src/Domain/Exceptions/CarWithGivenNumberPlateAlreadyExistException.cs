namespace Domain.Exceptions;

public class CarWithGivenNumberPlateAlreadyExistException : Exception
{
    public CarWithGivenNumberPlateAlreadyExistException(string numberPlate)
    : base($"Car with {numberPlate} number plate already exist")
    { }
}
