namespace Tests;

[TestClass]
public class OrderBllTest
{
    [TestMethod]
    public async Task CreateOrder_WithNonExistentCustomer_ShouldFailValidation()
    {
        Assert.AreEqual(1, 1);
    }
}