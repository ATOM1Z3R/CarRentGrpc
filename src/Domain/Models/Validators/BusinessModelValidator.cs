using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Models.Validators;

public static class BusinessModelValidator
{
    public static void Validate(IValidatableObject model, DataValidation dataValidation)
    {
        var validateAllProperties = dataValidation == DataValidation.All;
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(model, null, null);
        
        if (!Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties))
        {
            var validationResult = validationResults.First();
            throw new Exception(validationResult.ErrorMessage);
        }
    }
}
