using System.Globalization;
using kursa4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kursa4.Controllers;

public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;


    public AdminController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _context.Users.ToListAsync();
        var orders = await _context.Orders.ToListAsync();
        var laptops = await _context.Laptops.ToListAsync();
        
        ViewBag.UserCount = users.Count;
        ViewBag.OrderCount = orders.Count;
        ViewBag.LaptopCount = laptops.Count;

        return View();
    }

    public async Task<IActionResult> Laptops()
    {
        var laptops = await _context.Laptops
            .Include(l => l.Category)
            .Include(l => l.Brand)
            .Include(l => l.CPU)
            .Include(l => l.GPU)
            .Include(l => l.RAM)
            .Include(l => l.Storage)
            .ToListAsync();
        
        ViewBag.Categories = await _context.Categories.ToListAsync();
        ViewBag.Brands = await _context.Brands.ToListAsync();
        ViewBag.CPUs = await _context.CPUs.ToListAsync();
        ViewBag.GPUs = await _context.GPUs.ToListAsync();
        ViewBag.RAMs = await _context.RAMs.ToListAsync();
        ViewBag.Storages = await _context.Storages.ToListAsync();
        
        return View(laptops);
    }

    public async Task<IActionResult> Users()
    {
        var users = await _context.Users.ToListAsync();

        return View(users);
    }

    public async Task<IActionResult> Orders()
    {
        var orders = await _context.Orders
            .Include(o => o.User)
            .ToListAsync();
        
        return View(orders);
    }

    [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> AddLaptop(IFormCollection form, IFormFile ImageFile)
{
    var laptop = new Laptop();

    // 1. Парсим поля вручную из form
    laptop.Model = form["Model"];
    laptop.BrandId = int.Parse(form["BrandId"]);
    laptop.CategoryId = int.Parse(form["CategoryId"]);
    laptop.CPUId = int.Parse(form["CPUId"]);
    laptop.GPUId = int.Parse(form["GPUId"]);
    laptop.RamId = int.Parse(form["RamId"]);
    laptop.StorageId = int.Parse(form["StorageId"]);
    laptop.OS = form["OS"];
    laptop.ReleaseYear = int.Parse(form["ReleaseYear"]);
    laptop.BatteryWh = int.Parse(form["BatteryWh"]);
    laptop.ScreenSize = float.Parse(form["ScreenSize"], CultureInfo.InvariantCulture);
    laptop.RefreshRate = int.Parse(form["RefreshRate"]);
    laptop.Resolution = form["Resolution"];
    laptop.Width = float.Parse(form["Width"], CultureInfo.InvariantCulture);
    laptop.Height = float.Parse(form["Height"], CultureInfo.InvariantCulture);
    laptop.Depth = float.Parse(form["Depth"], CultureInfo.InvariantCulture);
    laptop.Weight = float.Parse(form["Weight"], CultureInfo.InvariantCulture);
    laptop.StockQuantity = int.Parse(form["StockQuantity"]);
    laptop.Description = form["Description"];

    // 2. Парсим цену — заменяем запятую на точку и используем инвариантную культуру
    var priceStr = form["Price"].ToString().Replace(',', '.');
    if (decimal.TryParse(priceStr, NumberStyles.Any, CultureInfo.InvariantCulture, out var price))
    {
        laptop.Price = price;
    }
    else
    {
        ModelState.AddModelError("Price", "Неверный формат цены.");
        return View(); // или верни на форму с ошибкой
    }

    // 3. Обработка изображения
    if (ImageFile != null && ImageFile.Length > 0)
    {
        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "laptops");
        Directory.CreateDirectory(uploadsFolder); // на случай если папки нет
        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await ImageFile.CopyToAsync(stream);
        }

        laptop.ImageUrl = "/images/laptops/" + uniqueFileName;
    }

    // 4. Сохраняем в БД
    _context.Laptops.Add(laptop);
    await _context.SaveChangesAsync();

    return RedirectToAction("Index");
}

    [HttpGet]
    public IActionResult DeleteLaptop(int id)
    {
        var laptop = _context.Laptops
            .Include(l => l.Brand)
            .FirstOrDefault(l => l.Id == id);

        if (laptop == null)
        {
            return NotFound();
        }
        
        return View(laptop);
    }

    [HttpPost, ActionName("DeleteLaptop")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteLaptopConfirmed(int id)
    {
        var laptop = _context.Laptops.Find(id);
        if (laptop != null)
        {
            _context.Laptops.Remove(laptop);
            _context.SaveChanges();
        }
        
        return RedirectToAction("Laptops");
    }
}