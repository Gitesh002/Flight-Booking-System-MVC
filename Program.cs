using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using fbs.Data;
using System.Text.Json;
using System;
using fbs.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FlightBookingConnectionString")));


builder.Services.AddDistributedMemoryCache(); // This stores session in memory
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true; // Ensures cookie is accessible only via HTTP
    options.Cookie.IsEssential = true; // Makes the session cookie essential
});


var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

//    // Check if the database is empty, seed if needed
//    if (!dbContext.Airports.Any())
//    {
//        SeedAirports(dbContext);
//    }
//}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession(); // Add this line to enable session handling


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();



//void SeedAirports(ApplicationDbContext context)
//{
//// Read JSON file from wwwroot/data/airports.json
//var jsonFilePath = Path.Combine(app.Environment.WebRootPath, "data", "airports.json");
//var jsonString = File.ReadAllText(jsonFilePath);
//var options = new JsonSerializerOptions
//    {
//        PropertyNameCaseInsensitive = true
//    };

//var airportData = JsonSerializer.Deserialize<AirportsData>(jsonString, options);

//    // Insert the list of airports into the database
//    if (airportData != null && airportData.Airports.Any())
//{
//context.Airports.AddRange(airportData.Airports);
//context.SaveChanges();
//}
//}

//public class AirportsData
//{
//    public List<Airport> Airports { get; set; }
//}