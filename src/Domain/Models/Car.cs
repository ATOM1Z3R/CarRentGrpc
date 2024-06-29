using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Car : Base, IValidatableObject
{
    public string NumberPlate { get; set; } = string.Empty;

    public string Manufacturer { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    public string Color { get; set; } = string.Empty;

    public uint NumberOfSeats { get; set; }

    public uint Year { get; set; }

    public bool Availability { get; set; }

    public uint PriceMultiplier { get; set; }

    public int? LocalizationId { get; set; }

    public Localization? Localization { get; set; }

    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(NumberPlate))
        {
            yield return new ValidationResult("'NumberPlate' value must be provided");
        }

        if (string.IsNullOrEmpty(Manufacturer))
        {
            yield return new ValidationResult("'Manufacturer' value must be provided");
        }

        if (string.IsNullOrEmpty(Model))
        {
            yield return new ValidationResult("'Model' value must be provided");
        }

        if (string.IsNullOrEmpty(Color))
        {
            yield return new ValidationResult("'Color' value must be provided");
        }

        if (NumberOfSeats == 0)
        {
            yield return new ValidationResult("'NumberOfSeats' value must be provided");
        }

        if (Year == 0)
        {
            yield return new ValidationResult("'Year' value must be provided");
        }
    }
}