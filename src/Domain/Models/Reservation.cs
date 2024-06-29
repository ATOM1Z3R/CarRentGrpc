using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Reservation : Base, IValidatableObject
{
    public DateTime RentDate { get; set; }

    public DateTime ExpectingRetrieveDate { get; set; }

    public DateTime ActualRetrieveDate { get; set; }

    public int CarId { get; set; }

    public Car? Car { get; set; }

    public int CustomerId { get; set; }

    public Customer? Customer { get; set; }

    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (ExpectingRetrieveDate < RentDate)
        {
            yield return new ValidationResult("ExpectingRetrieveDate is earlier than RentDate");
        }

        if (ActualRetrieveDate > DateTime.MinValue && ActualRetrieveDate < RentDate)
        {
            yield return new ValidationResult("ActualRetrieveDate is earlier than RentDate");
        }
    }
}
