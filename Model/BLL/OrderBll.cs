namespace Model.BLL;

using Microsoft.EntityFrameworkCore;
using Model.Classes;

public class OrderBll
{
    public static async Task<IList<Order>> GetAllOrders(Context db){
        return await db.Orders.ToArrayAsync();
    }

    public static async Task<Order> GetOrder(int id, Context db){
        var order = await db.Orders.FindAsync(id)
            is Order o
                ? o
                : null;

        if(order == null){
            throw new Exception("Order not found!");
        }

        var orderItemBll = new OrderItemBll();
        order.Items = await OrderItemBll.GetAllItemsFromOrder(order.Id, db);

        return order;
    }

    public static async Task<IList<Order>> GetAllOrdersByCustomerId(int customerId, Context db){
        return await db.Orders
            .Where(order => order.CustomerId == customerId)
            .ToArrayAsync();
    }
    
    public static async Task<Order> CreateOrder(Order order, Context db){
        var customer = await CustomerBll.GetCustomer(order.CustomerId, db)
            is Customer c
                ? c
                : null;

        if(customer is null){
            throw new Exception("Customer not found!");
        }

        db.Orders.Add(order);
        db.OrderItems.AddRange(order.Items);
        await db.SaveChangesAsync();

        return order;
    }

    public static async Task DeleteOrder(int id, Context db){
        if (await db.Orders.FindAsync(id) is Order order)
        {
            db.Orders.Remove(order);
            await db.SaveChangesAsync();
        }
    }
}