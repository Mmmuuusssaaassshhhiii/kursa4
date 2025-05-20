using kursa4.Interfaces;
using kursa4.Models;

namespace kursa4.Mocks;

public class MockLaptop : IAllLaptops
{
    
    //private readonly ILaptopsCategory _laptopsCategory = new MockCategory();
    private readonly ILaptopsBrand _laptopsBrand = new MockBrand();
    private readonly ILaptopsCpu _laptopsCpu = new MockCpu();
    private readonly ILaptopsGpu _laptopsGpu = new MockGpu();
    private readonly ILaptopsRam _laptopsRam = new MockRam();
    private readonly ILaptopsStorage _laptopsStorage = new MockStorage();
    
    public IEnumerable<Laptop> Laptops
    {
        get
        {
            return new List<Laptop>
            {
                new Laptop
                {
                    Brand = _laptopsBrand.allBrands.First(),
                    Model = "ROG Strix SCAR 18 2025 G835LW-SA091",
                    CPU = _laptopsCpu.AllCPUs.First(),
                    GPU = _laptopsGpu.AllGPUs.First(),
                    RAM = _laptopsRam.AllRams.First(),
                    Storage = _laptopsStorage.AllStorages.First(),
                    ////Category = _laptopsCategory.AllCategories.First(),
                    ScreenSize = 18,
                    Resolution = "2560 x 1600",
                    RefreshRate = 240,
                    KeyboardBackLight = true,
                    HasWebcam = true,
                    Weight = 3480,
                    Width = 399,
                    Height = 298,
                    Depth = 32,
                    Price = 12020,
                    Description = "Машина, а не ноутбук",
                    ReleaseYear = 2025,
                    StockQuantity = 15,
                    ImageUrl = "/img/laptopAsusRog.jpg",
                    BatteryWh = 90,
                    OS = "без ОС",
                },
            };
        }
    }

    public Laptop GetLaptop(int carId)
    {
        throw new NotImplementedException();
    }
}