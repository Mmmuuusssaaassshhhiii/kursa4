namespace kursa4.Models;

public class RAM
{
    public int Id { get; set; }
    public string Type { get; set; }
    public int SizeGb { get; set; }
    
    public ICollection<Laptop> Laptops { get; set; }
}