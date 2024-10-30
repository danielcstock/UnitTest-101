namespace Tests;

using System;
using Faker;
using Model.Classes;

[TestClass]
public class CustomerBllTest
{
    [TestMethod]
    public async Task CreateCustomer_WithValidParameters_Should_Succeed()
    {
        // Arranger
        var rand = new Random();
        var customer = new Customer{
            Id = rand.Next(100),
            Name = Faker.Name.FullName(NameFormats.WithPrefix),
            DocumentId = "12345678910",
            Address = Faker.Address.StreetAddress(),
            Email = Faker.Internet.Email()
        };

        // Act 
        

        // Assert
        Assert.AreEqual(1,1);
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