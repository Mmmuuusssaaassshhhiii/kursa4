using kursa4.Models;

namespace kursa4.Interfaces;

public interface ILaptopsStorage
{
    IEnumerable<Storage> AllStorages { get; }
    Storage GetStorage(int id);
    void AddStorage(Storage storage);
    void UpdateStorage(Storage storage);
    void DeleteStorage(int id);
}