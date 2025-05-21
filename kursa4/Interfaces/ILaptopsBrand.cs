using kursa4.Models;

namespace kursa4.Interfaces;

public interface ILaptopsBrand
{
    IEnumerable<Brand> allBrands { get; }
    Brand GetBrand(int id);
    void AddBrand(Brand brand);
    void UpdateBrand(Brand brand);
    void DeleteBrand(int id);
}