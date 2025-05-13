using kursa4.Interfaces;
using kursa4.Models;

namespace kursa4.Mocks;

public class MockStorage: ILaptopsStorage
{
    public IEnumerable<Storage> AllStorages
    {
        get
        {
            return new List<Storage>
            {
                new Storage { Type = "SSD", SizeGb = 1024 },
                new Storage { Type = "HDD", SizeGb = 556 },
            };
        }
    }
}