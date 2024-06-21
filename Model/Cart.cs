using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KhumaloCraft.enums;

namespace KhumaloCraft.Model;

[Table("Cart")]
public class Cart
{
    [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    [Required]
    public int ProductId { get; set; }
    [ForeignKey("ProductId")]
    public Product Product { get; set; }
    [Required] public int UserId { get; set; }
}