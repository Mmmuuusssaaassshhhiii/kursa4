using kursa4.Interfaces;
using kursa4.Models;

namespace kursa4.Mocks;

public class GpuRepository : ILaptopsGpu
{
    private readonly ApplicationDbContext _context;

    public GpuRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<GPU> AllGPUs => _context.GPUs.ToList();

    public GPU GetGPU(int id) => _context.GPUs.Find(id);

    public void AddGPU(GPU gpu)
    {
        _context.GPUs.Add(gpu);
        _context.SaveChanges();
    }

    public void UpdateGPU(GPU gpu)
    {
        _context.GPUs.Update(gpu);
        _context.SaveChanges();
    }

    public void DeleteGPU(int id)
    {
        var gpu = _context.GPUs.Find(id);
        if (gpu != null)
        {
            _context.GPUs.Remove(gpu);
            _context.SaveChanges();
        }
    }
}