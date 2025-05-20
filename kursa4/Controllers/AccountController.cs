using kursa4.Models;
using Microsoft.AspNetCore.Mvc;

namespace kursa4.Controllers;

public class AccountController : Controller
{
    private static readonly User MockUser = new User
{
    Id = 1,
    FullName = "Иван Иванов",
    Email = "ivan@example.com",
    PhoneNumber = "+7 900 123-45-67",
    Role = "Пользователь"
};

public IActionResult Index()
{
    // Передаём пользователя в представление
    return View(MockUser);
}

// Пример экшена для отображения заказов
public IActionResult Orders()
{
    // Заглушка — список заказов
    var orders = new List<Order>
    {
        new Order { Id = 123, OrderDate = DateTime.Now.AddDays(-5), Status = "доставлен" },
        new Order { Id = 124, OrderDate = DateTime.Now.AddDays(-2), Status = "в обработке" }
    };

    return View(orders);
}

// Пример экшена для корзины
public IActionResult Cart()
{
    var cartItems = new List<CartItem>
    {
        new CartItem { Id = 1, Laptop = new Laptop { Model = "ASUS ROG Strix G18" }, Quantity = 1 },
        new CartItem { Id = 2, Laptop = new Laptop { Model = "MacBook Air M2" }, Quantity = 2 }
    };

    return View(cartItems);
}
}