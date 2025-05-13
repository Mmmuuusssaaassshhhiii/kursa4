namespace kursa4.Models;

public class OrderItem
{
    public int Id { get; set; }
    
    public int OrderId { get; set; }
    public Order Order { get; set; }
    
    public int LaptopId { get; set; }
    public Laptop Laptop { get; set; }
    
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}