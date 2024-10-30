using Microsoft.EntityFrameworkCore;
using Model.Classes;

public class Context : DbContext, IApplicationDbContext
{
    public Context(DbContextOptions<Context> options)
        : base(options) { }

    public DbContext Instance => this;
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Customer> Customers => Set<Customer>();

    DbSet<Order> IApplicationDbContext.Orders {get => throw new NotImplementedException(); set => Set<Order>();}
    DbSet<OrderItem> IApplicationDbContext.OrderItems { get => throw new NotImplementedException(); set => Set<OrderItem>(); }
    DbSet<Customer> IApplicationDbContext.Customers { get => throw new NotImplementedException(); set => Set<Customer>(); }
}