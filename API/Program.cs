using NSwag.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Model.Classes;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Context>(opt => opt.UseInMemoryDatabase("OrderService"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "OrderAPI";
    config.Title = "OrderAPI v1";
    config.Version = "v1";
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "OrderAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

var orderRequests = app.MapGroup("/orders");

orderRequests.MapGet("/", Order.GetAllOrders);
orderRequests.MapGet("/complete", Order.GetCompleteOrders);
orderRequests.MapGet("/{id}", Order.GetOrder);
orderRequests.MapPost("/", Order.CreateOrder);
orderRequests.MapPut("/{id}", Order.UpdateOrder);
orderRequests.MapDelete("/{id}", Order.DeleteOrder);

var customerRequests = app.MapGroup("/customers");
customerRequests.MapGet("/", Customer.GetAllCustomers);
customerRequests.MapGet("/{id}", Customer.GetCustomer);
customerRequests.MapPost("/", Customer.CreateCustomer);
customerRequests.MapPut("/{id}", Customer.UpdateCustomer);
customerRequests.MapDelete("/{id}", Customer.DeleteCustomer);

app.Run();