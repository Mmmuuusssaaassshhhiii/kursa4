using kursa4.Models;

namespace kursa4.Interfaces;

public interface ILaptopsBrand
{
    IEnumerable<Brand> allBrands { get; }
}