using kursa4.Models;

namespace kursa4.Interfaces;

public interface ILaptopsStorage
{
    IEnumerable<Storage> AllStorages { get; }
}