using NSwag.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Model.Classes;
using Model.BLL;

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

// orderRequests.MapGet("/", OrderBll.GetAllOrders);
// orderRequests.MapGet("/complete", OrderBll.GetCompleteOrders);
orderRequests.MapGet("/{id}", OrderBll.GetOrder);
orderRequests.MapPost("/", OrderBll.CreateOrder);
// orderRequests.MapPut("/{id}", OrderBll.UpdateOrder);
// orderRequests.MapDelete("/{id}", OrderBll.DeleteOrder);

var customerRequests = app.MapGroup("/customers");
// customerRequests.MapGet("/", CustomerBll.GetAllCustomers);
customerRequests.MapGet("/{id}", CustomerBll.GetCustomer);
customerRequests.MapPost("/", CustomerBll.CreateCustomer);
// customerRequests.MapPut("/{id}", CustomerBll.UpdateCustomer);
customerRequests.MapDelete("/{id}", CustomerBll.DeleteCustomer);

app.Run();