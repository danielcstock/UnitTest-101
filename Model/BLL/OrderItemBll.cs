namespace Model.BLL;

using Microsoft.EntityFrameworkCore;
using Model.Classes;

public class OrderItemBll(IApplicationDbContext context)
{
    private readonly IApplicationDbContext _context = context;
    public async Task<IList<OrderItem>> GetAllItemsFromOrder(int orderId){
        return await _context.OrderItems
            .Where(item => item.OrderId == orderId)
            .ToArrayAsync();
    }

    public async Task<IList<OrderItem>> GetOrderItem(int id){
        return await _context.OrderItems
            // .Where(OrderItem => OrderItem.Status == 3)k
            .ToArrayAsync();
    }

    public async Task<OrderItem> CreateOrderItem(OrderItem orderItem){
        _context.OrderItems.Add(orderItem);
        await _context.Instance.SaveChangesAsync();

        return orderItem;
    }

    public async Task UpdateOrderItem(int id, OrderItem inputOrderItem){
        var OrderItem = await _context.OrderItems.FindAsync(id);

        // if (OrderItem is not null) {
        //     OrderItem.Buyer.Name = inputOrderItem.Buyer.Name;
        //     OrderItem.Status = inputOrderItem.Status;

            await _context.Instance.SaveChangesAsync(); 
    }

    public async Task DeleteOrderItem(int id){
        if (await _context.OrderItems.FindAsync(id) is OrderItem orderItem)
        {
            _context.OrderItems.Remove(orderItem);
            await _context.Instance.SaveChangesAsync();
        }
    }
}
