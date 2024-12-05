using CarManagement.Validation;
using System.ComponentModel.DataAnnotations;

namespace CarManagement.Models
{
    public class CarModel
    {
        public int Id { get; set; }
        [Required]
        [ValidBrand]
        public string Brand { get; set; }
        [Required]
        [ValidBrand]
        public string Class { get; set; }
        [Required]
        public string ModelName { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 10)]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Model Code must be 10 alphanumeric characters.")]

        public string ModelCode { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Features { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }
        [Required]
        public DateTime DateOfManufacturing { get; set; }
        public bool Active { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Sort Order must be a numeric value.")]
        public int SortOrder { get; set; }
        [Required(ErrorMessage = "At least one image is required.")]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new[] { ".jpg", ".png", ".jpeg" })] 
        public string ModelImage { get; set; }


        // Constructor
        public CarModel()
        {
        }
    }
}
