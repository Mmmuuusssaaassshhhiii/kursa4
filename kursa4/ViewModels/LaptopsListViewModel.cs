using kursa4.Models;

namespace kursa4.ViewModels;

public class LaptopsListViewModel
{
    public IEnumerable<Laptop> allLaptops { get; set; }
    
    public string currCategory { get; set; }
}