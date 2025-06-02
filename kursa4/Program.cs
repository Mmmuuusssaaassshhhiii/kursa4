using kursa4;
using kursa4.Interfaces;
using kursa4.Mocks;
using kursa4.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Подключение к MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IAllLaptops, LaptopRepository>();
builder.Services.AddTransient<ILaptopsCategory, CategoryRepository>();
builder.Services.AddTransient<ILaptopsBrand, BrandRepository>();
builder.Services.AddTransient<ILaptopsCpu, CpuRepository>();
builder.Services.AddTransient<ILaptopsGpu, GpuRepository>();
builder.Services.AddTransient<ILaptopsRam, RamRepository>();
builder.Services.AddTransient<ILaptopsStorage, StorageRepository>();
builder.Services.AddTransient<IAllUsers, UserRepository>();   
builder.Services.AddTransient<IUserCart, UserCartRepository>();
builder.Services.AddTransient<IUsersOrders, UsersOrderRepository>();
builder.Services.AddTransient<IUsersReviews, ReviewRepository>();
builder.Services.AddMvc();
builder.Services.AddSession();

// ✅ Аутентификация — ДО builder.Build()
builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
    });

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Laptops}/{action=ListLaptops}/{id?}");

app.Run();