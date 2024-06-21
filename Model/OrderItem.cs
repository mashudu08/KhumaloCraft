namespace KhumaloCraft.Model;

public class OrderItem
{
    public int OrderItemId { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public string Quantity { get; set; }
    public decimal Price { get; set; }

    public Order Order { get; set; }
    public Product Product { get; set; }
}