using kursa4.Models;

namespace kursa4.Interfaces;

public interface ILaptopsRam
{
    IEnumerable<RAM> AllRams { get; }
    RAM GetRAM(int id);
    void AddRAM(RAM ram);
    void UpdateRAM(RAM ram);
    void DeleteRAM(int id);
}