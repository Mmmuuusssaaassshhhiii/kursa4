using kursa4.Models;

namespace kursa4.Interfaces;

public interface ILaptopsCategory
{
    IEnumerable<Category> AllCategories { get; }
    Category GetCategory(int id);
    void AddCategory(Category category);
    void UpdateCategory(Category category);
    void DeleteCategory(int id);
}