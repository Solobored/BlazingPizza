using BlazingPizza.Models;

namespace BlazingPizza.Data;

public class OrderState
{
    public Order Order { get; set; } = new Order();

    public void ResetOrder() => Order = new Order();
}
