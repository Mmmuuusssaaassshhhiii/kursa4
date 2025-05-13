using kursa4.Models;

namespace kursa4.Interfaces;

public interface ILaptopsRam
{
    IEnumerable<RAM> AllRams { get; }
}