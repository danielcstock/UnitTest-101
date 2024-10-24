namespace Tests;

using Microsoft.EntityFrameworkCore;
using Model.BLL;
using Model.Classes;

[TestClass]
public class CustomerBllTest
{
    [TestMethod]
    public async Task CreateCustomer_WithValidParameters_Should_Succeed()
    {
        Assert.AreEqual(0, 1);
    }

    [TestMethod]
    public async Task CreateCustomer_WithEmptyDocumentId_Should_ThrowException()
    {
        Assert.AreEqual(0, 1);
    }

    [TestMethod]
    public async Task DeleteCustomer_WithAssociatedOrder_Should_ThrowException()
    {
        Assert.AreEqual(0, 1);
    }
}