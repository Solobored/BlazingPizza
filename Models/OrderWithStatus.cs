using System;

namespace BlazingPizza.Models
{
    public class OrderWithStatus
    {
        public Order Order { get; set; } = null!;
        public string StatusText { get; set; } = "";

        // convenience aliases used in Razor pages
        public int OrderId => Order.OrderId;
        public DateTime CreatedTime => Order.CreatedTime;

        public static OrderWithStatus FromOrder(Order order)
        {
            return new OrderWithStatus
            {
                Order = order,
                StatusText = "Placed"
            };
        }
    }
}
