namespace Model.BLL;

using Microsoft.EntityFrameworkCore;
using Model.Classes;

public class OrderItemBll
{
    public static async Task<IList<OrderItem>> GetAllItemsFromOrder(int orderId, Context db){
        return await db.OrderItems
            .Where(item => item.OrderId == orderId)
            .ToArrayAsync();
    }

    public static async Task<IList<OrderItem>> GetOrderItem(int id, Context db){
        return await db.OrderItems
            // .Where(OrderItem => OrderItem.Status == 3)k
            .ToArrayAsync();
    }

    public static async Task<OrderItem> CreateOrderItem(OrderItem orderItem, Context db){
        db.OrderItems.Add(orderItem);
        await db.SaveChangesAsync();

        return orderItem;
    }

    public static async Task UpdateOrderItem(int id, OrderItem inputOrderItem, Context db){
        var OrderItem = await db.OrderItems.FindAsync(id);

        // if (OrderItem is not null) {
        //     OrderItem.Buyer.Name = inputOrderItem.Buyer.Name;
        //     OrderItem.Status = inputOrderItem.Status;

            await db.SaveChangesAsync(); 
    }

    public static async Task DeleteOrderItem(int id, Context db){
        if (await db.OrderItems.FindAsync(id) is OrderItem orderItem)
        {
            db.OrderItems.Remove(orderItem);
            await db.SaveChangesAsync();
        }
    }
}
