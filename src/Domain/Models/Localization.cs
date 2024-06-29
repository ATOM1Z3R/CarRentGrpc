using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Localization : Base, IValidatableObject
{
    public string City { get; set; } = string.Empty;

    public string Street { get; set; } = string.Empty;

    public ICollection<Car> Cars { get; set; } = new List<Car>();

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
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
