using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

public class DateGreaterThanAttribute : ValidationAttribute
{
    private readonly string _comparisonProperty;

    public DateGreaterThanAttribute(string comparisonProperty)
    {
        _comparisonProperty = comparisonProperty;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
       
        var comparisonProperty = validationContext.ObjectType.GetProperty(_comparisonProperty);
        if (comparisonProperty == null)
        {
            return new ValidationResult($"Property {_comparisonProperty} not found.");
        }

        var comparisonValue = comparisonProperty.GetValue(validationContext.ObjectInstance);

      
        if (value is DateTime currentValue && comparisonValue is DateTime comparisonDate)
        {
            if (currentValue <= comparisonDate)
            {
                return new ValidationResult(ErrorMessage ?? $"The date must be later than {_comparisonProperty}.");
            }
        }

        return ValidationResult.Success;
    }
}
