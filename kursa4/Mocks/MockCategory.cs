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
    
    public Category GetCategory(int id) => _context.Categories.Find(id);

    public void AddCategory(Category category)
    {
        _context.Categories.Add(category);
        _context.SaveChanges();
    }

    public void UpdateCategory(Category category)
    {
        _context.Categories.Update(category);
        _context.SaveChanges();
    }

    public void DeleteCategory(int id)
    {
        var category = _context.Categories.Find(id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}
