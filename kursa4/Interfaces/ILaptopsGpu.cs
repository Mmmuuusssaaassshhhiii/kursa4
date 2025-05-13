using kursa4.Models;

namespace kursa4.Interfaces;

public interface ILaptopsGpu
{
    IEnumerable<GPU> AllGPUs { get; }
}