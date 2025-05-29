using kursa4.Interfaces;
using kursa4.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kursa4.Controllers;

public class LaptopsController : Controller
{
    
    private readonly ApplicationDbContext _context;
    private readonly IAllLaptops _allLaptops;
    private readonly ILaptopsCategory _laptopsCategory;
    private readonly ILaptopsBrand _laptopsBrand;
    private readonly ILaptopsCpu _laptopsCpu;
    private readonly ILaptopsGpu _laptopsGpu;
    private readonly ILaptopsRam _laptopsRam;
    private readonly ILaptopsStorage _laptopsStorage;

    public LaptopsController(
        ApplicationDbContext context,
        IAllLaptops iAllLaptops,
        ILaptopsCategory iLaptopsCategory,
        ILaptopsBrand iLaptopsBrand,
        ILaptopsCpu iLaptopCpu,
        ILaptopsGpu iLaptopsGpu,
        ILaptopsRam iLaptopsRam,
        ILaptopsStorage iLaptopsStorage)
    {
        _context = context;
        _allLaptops = iAllLaptops;
        _laptopsCategory = iLaptopsCategory;
        _laptopsBrand = iLaptopsBrand;
        _laptopsCpu = iLaptopCpu;
        _laptopsGpu = iLaptopsGpu;
        _laptopsRam = iLaptopsRam;
        _laptopsStorage = iLaptopsStorage;
    }

    public ViewResult ListLaptops(
    string category,
    string brand,
    string CPU,
    string GPU,
    string ramType,
    int? ramSize,
    string storageType,
    int? storageSize,
    decimal? priceFrom,
    decimal? priceTo,
    string sortBy)
{
    ViewBag.Title = "Каталог";

    var laptops = _allLaptops.Laptops.AsQueryable();

    if (!string.IsNullOrEmpty(category))
        laptops = laptops.Where(l => l.Category.Name == category);

    if (!string.IsNullOrEmpty(brand))
        laptops = laptops.Where(l => l.Brand.Name == brand);

    if (!string.IsNullOrEmpty(CPU))
        laptops = laptops.Where(l => l.CPU.Name == CPU);

    if (!string.IsNullOrEmpty(GPU))
        laptops = laptops.Where(l => l.GPU.Name == GPU);

    if (!string.IsNullOrEmpty(ramType))
        laptops = laptops.Where(l => l.RAM.Type == ramType);

    if (ramSize.HasValue)
        laptops = laptops.Where(l => l.RAM.SizeGb == ramSize.Value);

    if (!string.IsNullOrEmpty(storageType))
        laptops = laptops.Where(l => l.Storage.Type == storageType);

    if (storageSize.HasValue)
        laptops = laptops.Where(l => l.Storage.SizeGb == storageSize.Value);

    if (priceFrom.HasValue)
        laptops = laptops.Where(l => l.Price >= priceFrom.Value);

    if (priceTo.HasValue)
        laptops = laptops.Where(l => l.Price <= priceTo.Value);

    laptops = sortBy switch
    {
        "priceAsc" => laptops.OrderBy(l => l.Price),
        "priceDesc" => laptops.OrderByDescending(l => l.Price),
        _ => laptops
    };

    var viewModel = new LaptopsListViewModel
    {
        allLaptops = laptops.ToList(),
        currCategory = "Ноутбуки"
    };

    ViewBag.Categories = _laptopsCategory.AllCategories;
    ViewBag.Brands = _laptopsBrand.allBrands;
    ViewBag.CPUs = _laptopsCpu.AllCPUs;
    ViewBag.GPUs = _laptopsGpu.AllGPUs;
    ViewBag.Rams = _laptopsRam.AllRams;
    ViewBag.Storages = _laptopsStorage.AllStorages;

    return View(viewModel);
}


    public IActionResult Details(int id)
    {
        var laptop = _context.Laptops
            .Include(l => l.Category)
            .Include(l => l.Brand)
            .Include(l => l.CPU)
            .Include(l => l.GPU)
            .Include(l => l.RAM)
            .Include(l => l.Storage)
            .Include(l => l.Reviews)
            .ThenInclude(r => r.User)
            .FirstOrDefault(l => l.Id == id);

        if (laptop == null)
        {
            return NotFound();
        }
        
        return View(laptop);
    }
}