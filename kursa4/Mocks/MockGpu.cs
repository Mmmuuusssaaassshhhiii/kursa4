using kursa4.Interfaces;
using kursa4.Models;

namespace kursa4.Mocks;

public class MockGpu : ILaptopsGpu
{
    public IEnumerable<GPU> AllGPUs
    {
        get
        {
            return new List<GPU>
            {
                new GPU { Name = "NVIDIA GeForce RTX 5080 16 ГБ" },
                new GPU { Name = "Встроенная" }
            };
        }
    }
}