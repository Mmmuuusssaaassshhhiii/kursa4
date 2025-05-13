namespace kursa4.Models;

public class CPU
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public ICollection<Laptop> Laptops { get; set; }
}