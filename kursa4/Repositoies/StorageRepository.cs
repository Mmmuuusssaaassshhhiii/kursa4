using kursa4.Interfaces;
using kursa4.Models;

namespace kursa4.Mocks;

public class StorageRepository: ILaptopsStorage
{
    private readonly ApplicationDbContext _context;

    public StorageRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Storage> AllStorages => _context.Storages.ToList();

    public Storage GetStorage(int id) => _context.Storages.Find(id);

    public void AddStorage(Storage storage)
    {
        _context.Storages.Add(storage);
        _context.SaveChanges();
    }

    public void UpdateStorage(Storage storage)
    {
        _context.Storages.Update(storage);
        _context.SaveChanges();
    }

    public void DeleteStorage(int id)
    {
        var storage = _context.Storages.Find(id);
        if (storage != null)
        {
            _context.Storages.Remove(storage);
            _context.SaveChanges();
        }
    }
}