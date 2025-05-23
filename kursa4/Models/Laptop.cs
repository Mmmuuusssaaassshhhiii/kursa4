using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kursa4.Models;

public class Laptop
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Model { get; set; }
    
    public int BrandId { get; set; }
    public Brand Brand { get; set; }
    
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    
    public int CPUId { get; set; }
    public CPU CPU { get; set; }
    
    public int RamId { get; set; }
    public RAM RAM { get; set; }
    
    public int StorageId { get; set; }
    public Storage Storage { get; set; }
    
    public int GPUId { get; set; }
    public GPU GPU { get; set; }
    
    public string OS { get; set; }
    
    [Range(0, 1000)]
    public int BatteryWh { get; set; }
    
    [Range(0, 50)]
    public float ScreenSize { get; set; }
    
    public int RefreshRate { get; set; }
    public string Resolution { get; set; }
    
    [Range(0, 10)]
    public float Weight { get; set; }
    
    public float Width { get; set; }
    public float Height { get; set; }
    public float Depth { get; set; }
    
    public bool KeyboardBackLight { get; set; }
    public bool HasWebcam { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }
    
    public string ImageUrl { get; set; }
    public int StockQuantity { get; set; }
    public int ReleaseYear { get; set; }
    
    public string Description { get; set; }
    
    public ICollection<Review> Reviews { get; set; }
}