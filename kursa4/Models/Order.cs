namespace kursa4.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public string Status { get; set; } // pending, processing, shipped, completed, canceled
    
    public int UserId { get; set; }
    public User User { get; set; }
    
    public ICollection<OrderItem> OrderItems { get; set; }
    public Payment Payment { get; set; }
}