using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CarManagement.Validation
{
    public class ValidBrandAttribute : ValidationAttribute
    {
        private static readonly string[] ValidBrands = { "Audi", "Jaguar", "Land Rover", "Renault" };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Brand is required.");

            if (!ValidBrands.Contains(value.ToString()))
                return new ValidationResult($"Invalid brand. Allowed values are: {string.Join(", ", ValidBrands)}.");

            return ValidationResult.Success;
        }
    }
}
