using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazingPizza.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        // alias if code expects .Id for orders (optional)
        public int Id
        {
            get => OrderId;
            set => OrderId = value;
        }

        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;

        public List<Pizza> Pizzas { get; set; } = new();

        public decimal GetTotalPrice() => Pizzas.Sum(p => p.GetFormattedTotalPrice());
        public decimal GetFormattedTotalPrice() => GetTotalPrice();
    }
}
