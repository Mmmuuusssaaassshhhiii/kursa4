using kursa4.Models;

namespace kursa4.Interfaces;

public interface IAllLaptops
{
    IEnumerable<Laptop> Laptops { get; }
    Laptop GetLaptop(int laptopId);

    void AddLaptop(Laptop laptop);              // Добавить
    void UpdateLaptop(Laptop laptop);           // Обновить
    void DeleteLaptop(int laptopId);            // Удалить
}