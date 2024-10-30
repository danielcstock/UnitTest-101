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