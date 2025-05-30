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
    
    public IActionResult Filter(string search, string category, string brand, string priceFrom, string priceTo, string cpu, string gpu, string ramType, string ramSize, string storageType, string storageSize, string sortBy)
{
    // логика фильтрации ноутбуков
    var filtered = _context.Laptops
        .Include(x => x.Brand)
        .Include(x => x.Category)
        .Include(x => x.CPU)
        .Include(x => x.GPU)
        .Include(x => x.RAM)
        .Include(x => x.Storage)
        .AsQueryable();

    if (!string.IsNullOrWhiteSpace(search))
        filtered = filtered.Where(x =>
            (!string.IsNullOrEmpty(x.Model) && x.Model.ToLower().Contains(search.ToLower())) ||
            (x.Brand != null && !string.IsNullOrEmpty(x.Brand.Name) && x.Brand.Name.ToLower().Contains(search.ToLower()))
        );



    if (!string.IsNullOrWhiteSpace(category))
        filtered = filtered.Where(x => x.Category.Name == category);

    if (!string.IsNullOrWhiteSpace(brand))
        filtered = filtered.Where(x => x.Brand.Name == brand);

    if (!string.IsNullOrWhiteSpace(priceFrom) && decimal.TryParse(priceFrom, out var from))
        filtered = filtered.Where(x => x.Price >= from);

    if (!string.IsNullOrWhiteSpace(priceTo) && decimal.TryParse(priceTo, out var to))
        filtered = filtered.Where(x => x.Price <= to);

    if (!string.IsNullOrWhiteSpace(cpu))
        filtered = filtered.Where(x => x.CPU.Name == cpu);

    if (!string.IsNullOrWhiteSpace(gpu))
        filtered = filtered.Where(x => x.GPU.Name == gpu);

    if (!string.IsNullOrWhiteSpace(ramType))
        filtered = filtered.Where(x => x.RAM.Type == ramType);

    if (!string.IsNullOrWhiteSpace(ramSize) && int.TryParse(ramSize, out var ramSizeGb))
        filtered = filtered.Where(x => x.RAM.SizeGb == ramSizeGb);

    if (!string.IsNullOrWhiteSpace(storageType))
        filtered = filtered.Where(x => x.Storage.Type == storageType);

    if (!string.IsNullOrWhiteSpace(storageSize) && int.TryParse(storageSize, out var storageSizeGb))
        filtered = filtered.Where(x => x.Storage.SizeGb == storageSizeGb);

    if (!string.IsNullOrWhiteSpace(sortBy))
    {
        if (sortBy == "priceAsc")
            filtered = filtered.OrderBy(x => x.Price);
        else if (sortBy == "priceDesc")
            filtered = filtered.OrderByDescending(x => x.Price);
    }

    return PartialView("_LaptopListPartial", filtered.ToList());
}

    public ViewResult ListLaptops()
{
    ViewBag.Title = "Каталог";

    var laptops = _allLaptops.Laptops.AsQueryable();

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