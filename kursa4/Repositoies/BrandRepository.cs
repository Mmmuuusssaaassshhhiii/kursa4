using kursa4.Interfaces;
using kursa4.Models;

namespace kursa4.Mocks;

public class BrandRepository : ILaptopsBrand
{
    private readonly ApplicationDbContext _context;

    public BrandRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Brand> allBrands => _context.Brands.ToList();
    
    public Brand GetBrand(int id) => _context.Brands.Find(id);

    public void AddBrand(Brand brand)
    {
        _context.Brands.Add(brand);
        _context.SaveChanges();
    }

    public void UpdateBrand(Brand brand)
    {
        _context.Brands.Update(brand);
        _context.SaveChanges();
    }

    public void DeleteBrand(int id)
    {
        var brand = _context.Brands.Find(id);
        if (brand != null) 
        {
            _context.Brands.Remove(brand);
            _context.SaveChanges();
        }
    }
}