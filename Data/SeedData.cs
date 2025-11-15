using Microsoft.EntityFrameworkCore;
using BlazingPizza.Models;

namespace BlazingPizza.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new PizzaStoreContext(
            serviceProvider.GetRequiredService<DbContextOptions<PizzaStoreContext>>());

        if (context.Specials.Any())
        {
            return; // DB seeded
        }

        var specials = new List<PizzaSpecial>
        {
            new PizzaSpecial { Name="Margherita", Description="Classic tomato & cheese", Price=5.99M },
            new PizzaSpecial { Name="Pepperoni", Description="Pepperoni & extra cheese", Price=7.99M },
            new PizzaSpecial { Name="Veggie", Description="Peppers, onions, mushrooms", Price=6.99M }
        };

        context.Specials.AddRange(specials);
        context.SaveChanges();
    }
}
