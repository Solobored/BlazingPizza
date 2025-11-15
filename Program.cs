using Microsoft.EntityFrameworkCore;
using BlazingPizza.Data;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Configure EF Core (SQLite)
builder.Services.AddDbContext<PizzaStoreContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BlazingPizzaContext")
                      ?? "Data Source=BlazingPizza.db"));

// App state
builder.Services.AddScoped<OrderState>();

// Register HttpClient for server-side components (base address must match app)
var baseAddress = builder.Configuration["AppBaseUrl"] ?? "http://localhost:5289/";
builder.Services.AddScoped(sp => new System.Net.Http.HttpClient { BaseAddress = new Uri(baseAddress) });

// Add controllers (API)
builder.Services.AddControllers();

var app = builder.Build();

// Ensure DB + seed
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<PizzaStoreContext>();
    context.Database.EnsureCreated();
    SeedData.Initialize(services);
}

// pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
