using kursa4.Interfaces;
using kursa4.Models;

namespace kursa4.Mocks;

public class MockCpu : ILaptopsCpu
{
    public IEnumerable<CPU> AllCPUs
    {
        get
        {
            return new List<CPU>
            {
                new CPU { Name = "Intel Core i5 12450HX" },
                new CPU { Name = "AMD Ryzen 5 7535HS" }
            };
        }
    }
}