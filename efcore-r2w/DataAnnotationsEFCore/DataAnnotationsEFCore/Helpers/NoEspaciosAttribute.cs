using System.ComponentModel.DataAnnotations;

namespace DataAnnotationsEFCore.Helpers;

public class NoEspaciosAttribute : ValidationAttribute
{
    public NoEspaciosAttribute()
    {
        ErrorMessage = "El campo {0} no puede contener espacios en blanco.";
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is string str && str.Contains(" "))
            return new ValidationResult(ErrorMessage);

        return ValidationResult.Success;
    }
}

