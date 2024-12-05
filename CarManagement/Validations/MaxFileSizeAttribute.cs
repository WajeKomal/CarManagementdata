using System.ComponentModel.DataAnnotations;

namespace CarManagement.Validation
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var files = value as List<IFormFile>;
            if (files != null)
            {
                foreach (var file in files)
                {
                    if (file.Length > _maxFileSize)
                        return new ValidationResult($"File size must not exceed {_maxFileSize / (1024 * 1024)} MB.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
