using kursa4.Interfaces;
using kursa4.Models;

namespace kursa4.Mocks;

public class RamRepository : ILaptopsRam
{
    private readonly ApplicationDbContext _context;

    public RamRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<RAM> AllRams => _context.RAMs.ToList();

    public RAM GetRAM(int id) => _context.RAMs.Find(id);

    public void AddRAM(RAM ram)
    {
        _context.RAMs.Add(ram);
        _context.SaveChanges();
    }

    public void UpdateRAM(RAM ram)
    {
        _context.RAMs.Update(ram);
        _context.SaveChanges();
    }

    public void DeleteRAM(int id)
    {
        var ram = _context.RAMs.Find(id);
        if (ram != null)
        {
            _context.RAMs.Remove(ram);
            _context.SaveChanges();
        }
    }
}