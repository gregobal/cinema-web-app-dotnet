using System.ComponentModel.DataAnnotations;

namespace CinemaWebApi.Validations;

public class FirstLetterUppercaseAttribute: ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var str = value?.ToString();
        
        if (string.IsNullOrEmpty(str))
        {
            return ValidationResult.Success;
        }
        
        var firstLetter = str[0].ToString();

        if (firstLetter != firstLetter.ToUpper())
        {
            return new ValidationResult("First letter should be uppercase");
        }

        return ValidationResult.Success;
    }
}