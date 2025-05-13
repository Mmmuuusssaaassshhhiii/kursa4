using kursa4.Interfaces;
using kursa4.Models;

namespace kursa4.Mocks;

public class MockRam : ILaptopsRam
{
    public IEnumerable<RAM> AllRams
    {
        get
        {
            return new List<RAM>
            {
                new RAM { Type = "DDR5", SizeGb = 16 },
                new RAM { Type = "DDR4", SizeGb = 16 },
            };
        }
    }
}