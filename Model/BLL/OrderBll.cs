namespace Model.BLL;

using Microsoft.EntityFrameworkCore;
using Model.Classes;

public class OrderBll(IApplicationDbContext context)
{
    private readonly IApplicationDbContext _context = context;

    public async Task<IList<Order>> GetAllOrders(){
        return await _context.Orders.ToArrayAsync();
    }

    public async Task<Order> GetOrder(int id){
        var order = await _context.Orders.FindAsync(id)
            is Order o
                ? o
                : null;

        if(order == null){
            throw new Exception("Order not found!");
        }

        var orderItemBll = new OrderItemBll(_context);
        order.Items = await orderItemBll.GetAllItemsFromOrder(order.Id);

        return order;
    }

    public async Task<IList<Order>> GetAllOrdersByCustomerId(int customerId){
        return await ((Context)(_context.Instance)).Orders
            .Where(order => order.CustomerId == customerId)
            .ToArrayAsync();
    }
    
    public async Task<Order> CreateOrder(Order order){
        ((Context)_context.Instance).Orders.Add(order);
        await _context.Instance.SaveChangesAsync();

        return order;
    }

    public async Task DeleteOrder(int id){
        if (await _context.Orders.FindAsync(id) is Order order)
        {
            _context.Orders.Remove(order);
            await _context.Instance.SaveChangesAsync();
        }
    }
}