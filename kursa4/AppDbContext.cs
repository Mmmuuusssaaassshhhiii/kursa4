using kursa4.Models;
using Microsoft.EntityFrameworkCore;

namespace kursa4;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Laptop> Laptops { get; set; }
}