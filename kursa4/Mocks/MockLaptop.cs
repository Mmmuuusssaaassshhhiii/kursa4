using kursa4.Interfaces;
using kursa4.Models;
using Microsoft.EntityFrameworkCore;

namespace kursa4.Mocks;

public class MockLaptop : IAllLaptops
{
    private readonly ApplicationDbContext _context;

    public MockLaptop(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Laptop> Laptops => _context.Laptops
        .Include(l => l.Brand)
        .Include(l => l.Category)
        .Include(l => l.CPU)
        .Include(l => l.GPU)
        .Include(l => l.RAM)
        .Include(l => l.Storage)
        .ToList();

    public Laptop GetLaptop(int laptopId)
    {
        return _context.Laptops
            .Include(l => l.Brand)
            .Include(l => l.Category)
            .Include(l => l.CPU)
            .Include(l => l.GPU)
            .Include(l => l.RAM)
            .Include(l => l.Storage)
            .FirstOrDefault(l => l.Id == laptopId);
    }

    public void AddLaptop(Laptop laptop)
    {
        _context.Laptops.Add(laptop);
        _context.SaveChanges();
    }

    public void UpdateLaptop(Laptop laptop)
    {
        _context.Laptops.Update(laptop);
        _context.SaveChanges();
    }

    public void DeleteLaptop(int id)
    {
        var laptop = _context.Laptops.Find(id);
        if (laptop != null)
        {
            _context.Laptops.Remove(laptop);
            _context.SaveChanges();
        }
    }
}