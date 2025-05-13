namespace kursa4.Models;

public class Review
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
    
    public int LaptopId { get; set; }
    public Laptop Laptop { get; set; }
    
    public string Content { get; set; }
    public int Rating { get; set; } //1-5 stars
    public DateTime CreatedAt { get; set; }
}