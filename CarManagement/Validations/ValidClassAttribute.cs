using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CarManagement.Validation
{
    public class ValidClassAttribute : ValidationAttribute
    {
        private static readonly string[] ValidClasses = { "A-Class", "B-Class", "C-Class" };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Class is required.");

            if (!ValidClasses.Contains(value.ToString()))
                return new ValidationResult($"Invalid class. Allowed values are: {string.Join(", ", ValidClasses)}.");

            return ValidationResult.Success;
        }
    }
}
