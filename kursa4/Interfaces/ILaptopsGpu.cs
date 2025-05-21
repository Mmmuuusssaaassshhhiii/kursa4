using kursa4.Models;

namespace kursa4.Interfaces;

public interface ILaptopsGpu
{
    IEnumerable<GPU> AllGPUs { get; }
    GPU GetGPU(int id);
    void AddGPU(GPU gpu);
    void UpdateGPU(GPU gpu);
    void DeleteGPU(int id);
}