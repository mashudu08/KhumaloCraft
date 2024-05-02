using System.ComponentModel.DataAnnotations;

namespace KhumaloCraft.Model
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Availability { get; set; }
    }
}
