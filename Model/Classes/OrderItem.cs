namespace Model.Classes;

using Microsoft.EntityFrameworkCore;

public class OrderItem
{
    public int Id {get;set;}
    public string Description {get;set;}
    public decimal Price {get;set;}
    public decimal Quantity {get;set;}

    public OrderItem(){
        Id = 1;
        Description = "Bonsai mini";
        Price = 49.90M;
        Quantity = 1;
    }

    public static async Task<IList<OrderItem>> GetAllItemsFromOrder(int id, Context db){
        return await db.OrderItems.ToArrayAsync();
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
