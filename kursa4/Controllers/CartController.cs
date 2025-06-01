using Microsoft.AspNetCore.Mvc;
using kursa4.Interfaces;
using kursa4.Models;

public class CartController : Controller
{
    private readonly IUserCart _cartRepo;
    private readonly IAllUsers _userRepo;

    public CartController(IUserCart cartRepo, IAllUsers userRepo)
    {
        _cartRepo = cartRepo;
        _userRepo = userRepo;
    }

    [HttpPost]
    public IActionResult AddToCart(int id) // id = LaptopId
    {
        Console.WriteLine($"[DEBUG] AddToCart вызван с laptopId={id}");

        var email = HttpContext.Session.GetString("UserEmail");
        Console.WriteLine($"[DEBUG] Email из сессии: {email}");

        if (string.IsNullOrEmpty(email))
        {
            Console.WriteLine("[DEBUG] Email пустой — пользователь неавторизован");
            return Unauthorized(); // Лучше чем редирект для JS
        }

        var user = _userRepo.GetUserByEmail(email);
        if (user == null)
        {
            Console.WriteLine("[DEBUG] Пользователь не найден по email");
            return NotFound();
        }

        Console.WriteLine($"[DEBUG] Добавляем в корзину: userId={user.Id}, laptopId={id}");
        _cartRepo.AddItemToCart(user.Id, id, 1);

        return Ok();
    }

    public IActionResult Index()
    {
        var email = HttpContext.Session.GetString("UserEmail");
        if (string.IsNullOrEmpty(email)) return RedirectToAction("Login", "Account");

        var user = _userRepo.GetUserByEmail(email);
        if (user == null) return RedirectToAction("Login", "Account");

        var items = _cartRepo.GetCartItems(user.Id).ToList();
        return View(items); // Представление см. ниже
    }
}