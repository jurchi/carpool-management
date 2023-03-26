using CarpoolManagement.Persistance;
using CarpoolManagement.Source;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddProblemDetails(options =>
{
    options.Map<BadHttpRequestException>(ex => new ProblemDetails
    {
        Title = "Bad Request",
        Status = StatusCodes.Status400BadRequest,
        Detail = ex.Message
    });

    options.Map<KeyNotFoundException>(ex => new ProblemDetails
    {
        Title = "Not Found",
        Status = StatusCodes.Status404NotFound,
        Detail = ex.Message
    });

    options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
});

string connectionString = builder.Configuration.GetConnectionString("CarpoolDbConnection")!;
builder.Services.AddDbContext<CarpoolContext>(options => options.UseSqlite(connectionString));

builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

builder.Services.AddScoped<RideShareService>();
builder.Services.AddScoped<CarService>();
builder.Services.AddScoped<EmployeeService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v0.1",
        Title = "Carpool Managment API",
        Description = "A.NET API for carpool management"
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseProblemDetails();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
