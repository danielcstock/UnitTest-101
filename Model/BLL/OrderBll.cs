namespace Model.BLL;

using Microsoft.EntityFrameworkCore;
using Model.Classes;

public class OrderBll
{
    public static async Task<IList<Order>> GetAllOrders(Context db){
        var order = await db.Orders.ToArrayAsync();
        var customerBll = new CustomerBll();
        var orderItemBll = new OrderItemBll();

        // order.Customer = customerBll.GetCustomer(order.CustomerId, db);
        // order.Items = orderItemService.GetAllItemsFromOrder(order.Id, db);
        return null;
    }

    public static async Task<IList<Order>> GetCompleteOrders(Context db){
        return await db.Orders
            .Where(order => order.Status == 3)
            .ToArrayAsync();
    }

    public static async Task<Order> GetOrder(int id, Context db){
        return await db.Orders.FindAsync(id)
            is Order order
                ? order
                : null;
    }

    public static async Task<IList<Order>> GetOrderByCustomerId(int customerId, Context db){
        return await db.Orders
            .Where(order => order.CustomerId == customerId)
            .ToArrayAsync();
    }
    
    public static async Task<Order> CreateOrder(Order order, Context db){
        db.Orders.Add(order);
        db.OrderItems.AddRange(order.Items);
        await db.SaveChangesAsync();

        return order;
    }

    public static async Task UpdateOrder(int id, Order inputOrder, Context db){
        var order = await db.Orders.FindAsync(id);

        if (order is not null) {
            // order.Buyer.Name = inputOrder.Buyer.Name;
            order.Status = inputOrder.Status;

            await db.SaveChangesAsync();
        } 
    }

    public static async Task DeleteOrder(int id, Context db){
        if (await db.Orders.FindAsync(id) is Order order)
        {
            db.Orders.Remove(order);
            await db.SaveChangesAsync();
        }
    }
}