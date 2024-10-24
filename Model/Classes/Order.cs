namespace Model.Classes;

using Microsoft.EntityFrameworkCore;
using Model.BLL;

public class Order
{
    public int Id {get;set;}
    public DateTime Date {get;set;}
    public int Status {get;set;}
    public List<OrderItem> Items {get;set;}
    public int CustomerId {get;set;}
}