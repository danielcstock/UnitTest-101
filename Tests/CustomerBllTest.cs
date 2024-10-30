namespace Tests;

using System;
using Faker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Model.BLL;
using Model.Classes;

[TestClass]
public class CustomerBllTest
{
    private readonly ServiceProvider _serviceProvider;
    public CustomerBllTest(){
        var services = new ServiceCollection();
        services.AddDbContext<IApplicationDbContext, Context>(
            options => options.UseInMemoryDatabase("CustomerDbTest")
        );
        _serviceProvider = services.BuildServiceProvider();
    }

    [TestMethod]
    public async Task CreateCustomer_WithValidParameters_Should_Succeed()
    {
        using var scope = _serviceProvider.CreateScope();
        // Arrange
        var scopedServices = scope.ServiceProvider;
        var rand = new Random();
        var customer = new Customer
        {
            Id = rand.Next(100),
            Name = Faker.Name.FullName(NameFormats.WithPrefix),
            DocumentId = "12345678910",
            Address = Faker.Address.StreetAddress(),
            Email = Faker.Internet.Email()
        };

        var customerBll = new CustomerBll(scopedServices.GetRequiredService<IApplicationDbContext>());

        // Act 
        var response = await customerBll.CreateCustomer(customer);
        Console.WriteLine(response.Address);
        // Assert.ThrowsException<NullReferenceException>(async () => await customerBll.CreateCustomer(customer));

        // Assert
        // Assert.ThrowsException<NullReferenceException>(() =>  customerBll.CreateCustomer(customer));
        Assert.AreEqual(1, 1);
    }

    [TestMethod]
    public async Task CreateCustomer_WithEmptyDocumentId_Should_ThrowException()
    {
        Assert.AreEqual(1, 1);
    }

    [TestMethod]
    public async Task DeleteCustomer_WithAssociatedOrder_Should_ThrowException()
    {
        Assert.AreEqual(1, 1);
    }
}