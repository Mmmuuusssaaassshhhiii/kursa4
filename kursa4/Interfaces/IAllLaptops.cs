using kursa4.Models;

namespace kursa4.Interfaces;

public interface IAllLaptops
{
    IEnumerable<Laptop> Laptops { get; }
    Laptop GetLaptop(int carId);
}