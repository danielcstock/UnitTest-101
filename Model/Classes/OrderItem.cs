namespace Model.Classes;

using Microsoft.EntityFrameworkCore;

public class OrderItem
{
    public int Id {get;set;}
    public int OrderId {get;set;}
    public string Description {get;set;}
    public decimal Price {get;set;}
    public decimal Quantity {get;set;}
}