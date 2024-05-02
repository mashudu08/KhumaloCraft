using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace KhumaloCraft.Model
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        [ForeignKey("UserId")]
        public User user { get; set; }
        [ForeignKey("ProductId")]
        public Product product { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
    }
}
