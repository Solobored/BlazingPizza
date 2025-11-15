using Microsoft.EntityFrameworkCore;
using BlazingPizza.Data;

var builder = WebApplication.CreateBuilder(args);

// Razor + Blazor Server
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// EF Core (SQLite)
builder.Services.AddDbContext<PizzaStoreContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("BlazingPizzaContext")
        ?? "Data Source=BlazingPizza.db"
    )
);

// App state
builder.Services.AddScoped<OrderState>();

// *** THE IMPORTANT FIX ***
// This MUST be placed BEFORE builder.Build()
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5289/") // adjust if needed
});

// API controllers
builder.Services.AddControllers();

var app = builder.Build();

// DB + seed
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<PizzaStoreContext>();
    context.Database.EnsureCreated();
    SeedData.Initialize(services);
}

// Pipeline
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
