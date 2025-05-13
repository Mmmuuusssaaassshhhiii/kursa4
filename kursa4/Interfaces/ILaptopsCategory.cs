using kursa4.Models;

namespace kursa4.Interfaces;

public interface ILaptopsCategory
{
    IEnumerable<Category> AllCategories { get; }
}