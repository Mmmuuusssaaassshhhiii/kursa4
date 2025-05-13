namespace kursa4.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public ICollection<Laptop> Laptops { get; set; }
}