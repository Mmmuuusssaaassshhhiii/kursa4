using kursa4.Interfaces;
using kursa4.Models;

namespace kursa4.Mocks;

public class MockCategory : ILaptopsCategory
{
    public IEnumerable<Category> AllCategories
    {
        get
        {
            return new List<Category>
            {
                new Category { Name = "Игровые", Description = "Мощные ноутбуки, на них ты будешь апать ммры)" },
                new Category { Name = "Для работы", Description = "Эти ноутбуки не стыдно взять с собой в кофейню, чтобы сидеть пить лавандовый раф на альтернативном молоке и параллеьно заниматься фрилансом"}
            };
        }
    } 
}
