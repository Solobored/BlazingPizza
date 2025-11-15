using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazingPizza.Models;

public class Pizza
{
    public int Id { get; set; }

    public int SpecialId { get; set; }
    public PizzaSpecial Special { get; set; } = default!;

    public string Size { get; set; } = "Medium";

    public List<PizzaTopping> Toppings { get; set; } = new();
    
    public decimal GetFormattedTotalPrice()
    {
        return Special.Price + Toppings.Sum(t => t.Topping.Price);
    }
}
