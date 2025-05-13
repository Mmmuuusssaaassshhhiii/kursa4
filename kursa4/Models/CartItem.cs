namespace kursa4.Models;

public class CartItem
{
    public int Id { get; set; }
    
    public int CartId { get; set; }
    public Cart Cart { get; set; } = null!;
    
    public int LaptopId { get; set; }
    public Laptop Laptop { get; set; } = null!;
    
    public int Quantity { get; set; }
}