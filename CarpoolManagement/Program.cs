using CarpoolManagement.Persistance.Repository;
using CarpoolManagement.Source;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<RideShareService>();
builder.Services.AddScoped<CarRepository>();
builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddSingleton<RideShareRepository>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
