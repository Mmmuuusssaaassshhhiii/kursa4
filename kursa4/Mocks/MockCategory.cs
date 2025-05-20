using kursa4.Interfaces;
using kursa4.Models;

namespace kursa4.Mocks;

public class MockCategory : ILaptopsCategory
{
    private readonly ApplicationDbContext _context;

    public MockCategory(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Category> AllCategories => _context.Categories.ToList(); 
}
