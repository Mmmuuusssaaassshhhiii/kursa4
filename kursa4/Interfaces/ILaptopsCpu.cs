using kursa4.Models;

namespace kursa4.Interfaces;

public interface ILaptopsCpu
{
    IEnumerable<CPU> AllCPUs { get; }
    CPU GetCPU(int id);
    void AddCPU(CPU cpu);
    void UpdateCPU(CPU cpu);
    void DeleteCPU(int id);
}