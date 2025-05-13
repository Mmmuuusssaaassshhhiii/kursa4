using kursa4.Interfaces;
using kursa4.Mocks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IAllLaptops, MockLaptop>();
builder.Services.AddTransient<ILaptopsCategory, MockCategory>();
builder.Services.AddTransient<ILaptopsBrand, MockBrand>();
builder.Services.AddTransient<ILaptopsCpu, MockCpu>();
builder.Services.AddTransient<ILaptopsGpu, MockGpu>();
builder.Services.AddTransient<ILaptopsRam, MockRam>();
builder.Services.AddTransient<ILaptopsStorage, MockStorage>();
builder.Services.AddMvc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.UseStaticFiles();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();