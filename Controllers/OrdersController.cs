using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazingPizza.Data;
using BlazingPizza.Models;

namespace BlazingPizza.Controllers;

[ApiController]
[Route("orders")]
public class OrdersController : ControllerBase
{
    private readonly PizzaStoreContext _db;

    public OrdersController(PizzaStoreContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<List<OrderWithStatus>>> GetOrders()
    {
        var orders = await _db.Orders
            .Include(o => o.Pizzas).ThenInclude(p => p.Special)
            .OrderByDescending(o => o.CreatedTime)
            .ToListAsync();

        return orders.Select(o => OrderWithStatus.FromOrder(o)).ToList();
    }

    [HttpGet("{orderId}")]
    public async Task<ActionResult<OrderWithStatus>> GetOrder(int orderId)
    {
        var order = await _db.Orders
            .Include(o => o.Pizzas).ThenInclude(p => p.Special)
            .SingleOrDefaultAsync(o => o.OrderId == orderId);

        if (order == null) return NotFound();
        return OrderWithStatus.FromOrder(order);
    }

    [HttpPost]
    public async Task<ActionResult<int>> PlaceOrder(Order order)
    {
        order.CreatedTime = DateTime.UtcNow;

        // If SpecialId was provided but Special is set, normalize
        foreach(var pizza in order.Pizzas)
        {
            if (pizza.Special != null)
{
        pizza.SpecialId = pizza.Special.Id;
    }

            }

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
            return order.OrderId;
        }
}
