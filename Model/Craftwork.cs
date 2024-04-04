using System.ComponentModel.DataAnnotations;

namespace KhumaloCraft.Models
{
    public class Craftwork
    {
        [Key]
        public int CraftId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string ImageURL { get; set; }
    }
}
