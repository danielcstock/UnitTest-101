using Microsoft.EntityFrameworkCore;
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
var orderBll = new OrderBll(null);

orderRequests.MapGet("/{id}", async (int id) => {
    return await orderBll.GetOrder(id);
});
orderRequests.MapPost("/", orderBll.CreateOrder);

var customerRequests = app.MapGroup("/customers");
var customerBll = new CustomerBll(null);

customerRequests.MapGet("/{id}", async (int id) => {
    return await customerBll.GetCustomer(id);
});
customerRequests.MapPost("/", customerBll.CreateCustomer);
customerRequests.MapDelete("/{id}", async (int id) => {
    await customerBll.DeleteCustomer(id);
});

app.Run();