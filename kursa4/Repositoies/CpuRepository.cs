using kursa4.Interfaces;
using kursa4.Models;

namespace kursa4.Mocks;

public class CpuRepository : ILaptopsCpu
{
  private readonly ApplicationDbContext _context;

  public CpuRepository(ApplicationDbContext context)
  {
    _context = context;
  }

  public IEnumerable<CPU> AllCPUs => _context.CPUs.ToList();

  public CPU GetCPU(int id) => _context.CPUs.Find(id);

  public void AddCPU(CPU cpu)
  {
    _context.CPUs.Add(cpu);
    _context.SaveChanges();
  }

  public void UpdateCPU(CPU cpu)
  {
    _context.CPUs.Update(cpu);
    _context.SaveChanges();
  }

  public void DeleteCPU(int id)
  {
    var cpu = _context.CPUs.Find(id);
    if (cpu != null)
    {
      _context.CPUs.Remove(cpu);
      _context.SaveChanges();
    }
  }
}