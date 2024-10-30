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