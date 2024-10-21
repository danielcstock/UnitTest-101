namespace Model.Classes;

using Microsoft.EntityFrameworkCore;

public class Order
{
    public int Id {get;set;}
    public DateTime Date {get;set;}
    public int Status {get;set;}
    public List<OrderItem> Items {get;set;}
    public Customer Buyer {get;set;}

    public Order(){
        var customer = new Customer(
            "Daniel Stock",
            "12345678910",
            "Rua Guararapes, 342",
            "daniel.stock@bravium.com.br"
        );

        var orderItemA = new OrderItem(
            "Bonsai mini",
            49.90M,
            1
        );

        var orderItemB = new OrderItem(
            "Mouse",
            99.90M,
            2
        );

        Id = 1;
        Date = DateTime.Now;
        Status = 1;
        Buyer = customer;
        Items = new List<OrderItem>(){
            orderItemA,
            orderItemB
        };
    }

    public static async Task<IList<Order>> GetAllOrders(Context db){
        return await db.Orders.ToArrayAsync();
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
            .Where(order => order.Buyer.Id == customerId)
            .ToArrayAsync();
    }
    
    public static async Task<Order> CreateOrder(Order order, Context db){
        db.Orders.Add(order);
        await db.SaveChangesAsync();

        return order;
    }

    public static async Task UpdateOrder(int id, Order inputOrder, Context db){
        var order = await db.Orders.FindAsync(id);

        if (order is not null) {
            order.Buyer.Name = inputOrder.Buyer.Name;
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