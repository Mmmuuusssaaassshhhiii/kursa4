using kursa4.Interfaces;
using kursa4.Models;

namespace kursa4.Mocks;

public class MockBrand : ILaptopsBrand
{
    private readonly ApplicationDbContext _context;

    public MockBrand(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Brand> allBrands => _context.Brands.ToList();
}