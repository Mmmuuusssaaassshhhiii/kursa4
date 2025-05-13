namespace kursa4.Models;

public class Payment
{
    public int Id { get; set; }
    
    public int OrderId { get; set; }
    public Order Order { get; set; }
    
    public decimal Amount { get; set; }
    public string Status { get; set; } // pending, paid, failed
    public DateTime PaymentDate { get; set; }
}