namespace CarManagement.Models
{
    public class CarModelImage
    {
        public int Id { get; set; }
        public int CarModelId { get; set; }  
        public string? ImagePath { get; set; }
        public bool IsPrimary { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
