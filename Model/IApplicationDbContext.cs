using Microsoft.EntityFrameworkCore;
using Model.Classes;

public interface IApplicationDbContext : IDBContext
{
    public DbSet<Order> Orders {get;set;}
    public DbSet<OrderItem> OrderItems {get;set;}
    public DbSet<Customer> Customers {get;set;}
}