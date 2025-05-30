using System.Security.Claims;
using kursa4.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace kursa4.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Вспомогательный метод для получения текущего пользователя
        private async Task<User?> GetCurrentUserAsync()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(email)) return null;

            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        // Главная страница личного кабинета
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            if (user == null) return RedirectToAction("Login");

            return View(user);
        }

        // Заказы
        public async Task<IActionResult> Orders()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(email)) return RedirectToAction("Login");

            var user = await _context.Users
                .Include(u => u.Orders)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null) return RedirectToAction("Login");

            return View(user.Orders.ToList());
        }

        // Корзина
        public async Task<IActionResult> Cart()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(email)) return RedirectToAction("Login");

            var user = await _context.Users
                .Include(u => u.Cart)
                .ThenInclude(c => c.CartItems)
                .ThenInclude(ci => ci.Laptop)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user?.Cart?.CartItems == null)
                return View(new List<CartItem>());

            return View(user.Cart.CartItems.ToList());
        }

        // Регистрация (GET)
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Регистрация (POST)
        [HttpPost]
        public async Task<IActionResult> Register(User model, string confirmPassword)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Password != confirmPassword)
            {
                ModelState.AddModelError("Password", "Пароли не совпадают");
                return View(model);
            }

            var existingUserByEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (existingUserByEmail != null)
            {
                ModelState.AddModelError("Email", "Пользователь с таким Email уже существует");
                return View(model);
            }

            var existingUserByPhone = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == model.PhoneNumber);
            if (existingUserByPhone != null)
            {
                ModelState.AddModelError("PhoneNumber", "Пользователь с таким номером телефона уже существует");
                return View(model);
            }

            model.Role = "User";
            model.Cart = new Cart(); // EF сам создаст CartItems при необходимости

            _context.Users.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Login");
        }

        // Вход (GET)
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Вход (POST)
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                var claimsIdentity = new ClaimsIdentity(claims, "MyCookieAuth");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // Вход (создание cookie)
                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);
                
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("UserName", user.FullName);
                HttpContext.Session.SetString("UserRole", user.Role);

                if (user.Role == "Admin")
                    return RedirectToAction("Index", "Admin");

                return RedirectToAction("Index", "Account");
            }

            ViewBag.Error = "Неверный email или пароль";
            return View();
        }

        // Выход
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("MyCookieAuth");
            HttpContext.Session.Clear();
            return RedirectToAction("ListLaptops", "Laptops");
        }
    }
}
