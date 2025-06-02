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
            return Unauthorized();
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

        // Загружаем CartItems с ноутбуками
        var items = _cartRepo.GetCartItems(user.Id)
            .Select(ci =>
            {
                ci.Laptop = _cartRepo.GetLaptopById(ci.LaptopId); // добавь этот метод в IUserCart, если нужно
                return ci;
            }).ToList();

        return View(items);
    }
    
    [HttpPost]
    public IActionResult UpdateCart(int[] ItemIds, int[] Quantities)
    {
        var email = HttpContext.Session.GetString("UserEmail");
        if (string.IsNullOrEmpty(email)) return RedirectToAction("Login", "Account");

        var user = _userRepo.GetUserByEmail(email);
        if (user == null) return RedirectToAction("Login", "Account");

        for (int i = 0; i < ItemIds.Length; i++)
        {
            _cartRepo.UpdateItemQuantity(user.Id, ItemIds[i], Quantities[i]);
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult RemoveItem(int itemId)
    {
        var email = HttpContext.Session.GetString("UserEmail");
        if (string.IsNullOrEmpty(email)) return RedirectToAction("Login", "Account");

        var user = _userRepo.GetUserByEmail(email);
        if (user == null) return RedirectToAction("Login", "Account");

        _cartRepo.RemoveItemFromCart(user.Id, itemId);

        return RedirectToAction("Index"); // ✔️ редирект обратно на корзину
    }
    
    [HttpPost]
    public IActionResult ClearCart()
    {
        var email = HttpContext.Session.GetString("UserEmail");
        if (string.IsNullOrEmpty(email)) return Unauthorized();

        var user = _userRepo.GetUserByEmail(email);
        if (user == null) return NotFound();

        _cartRepo.ClearCart(user.Id);
        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public IActionResult Checkout()
    {
        // Тут ты можешь создать заказ и очистить корзину, например
        // или редирект на страницу оформления
        return RedirectToAction("Create", "Orders");
    }
}
