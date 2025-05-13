using kursa4.Interfaces;
using kursa4.Models;

namespace kursa4.Mocks;

public class MockBrand : ILaptopsBrand
{
    public IEnumerable<Brand> allBrands
    {
        get
        {
            return new List<Brand>
            {
                new Brand { Name = "Asus", Description = "хахахахаахах, анус)))" },
                new Brand { Name = "Lenovo", Description = "ну ничё такой бренд" }
            };
        }
    }
}