using kursa4.Models;

namespace kursa4.Interfaces;

public interface ILaptopsCpu
{
    IEnumerable<CPU> AllCPUs { get; }
}