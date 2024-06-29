using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Customer : Person, IValidatableObject
{
    public string City { get; set; } = string.Empty;

    public string Street { get; set; } = string.Empty;

    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (PhoneNumber.Any(x => char.IsLetter(x)))
        {
            yield return new ValidationResult("Phone number cannot contain letters");
        }

        if (string.IsNullOrEmpty(PhoneNumber))
        {
            yield return new ValidationResult("'PhoneNumber' value must be provided");
        }

        if (!Email.Contains('@'))
        {
            yield return new ValidationResult("Email is not valid");
        }

        if (string.IsNullOrEmpty(FirstName))
        {
            yield return new ValidationResult("'FirstName' value must be provided");
        }

        if (string.IsNullOrEmpty(LastName))
        {
            yield return new ValidationResult("'LastName' value must be provided");
        }

        if (string.IsNullOrEmpty(City))
        {
            yield return new ValidationResult("'City' value must be provided");
        }

        if (string.IsNullOrEmpty(Street))
        {
            yield return new ValidationResult("'Street' value must be provided");
        }
    }
}
