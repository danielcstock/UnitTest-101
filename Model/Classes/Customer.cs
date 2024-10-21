namespace Model.Classes;

using Microsoft.EntityFrameworkCore;

public class Customer
{
    public int Id {get;set;}
    public string Name {get;set;}
    public string DocumentId {get;set;}
    public string Address {get;set;}
    public string Email {get;set;}

    public Customer(string name, string documentId, string address, string email){
        if(IsEmptyOrNull(name)){
            throw new ArgumentException("Invalid customer name!");
        };

        if(IsEmptyOrNull(address)){
            throw new ArgumentException("Invalid address name!");
        };
        
        if(!IsValidDocumentId(documentId)){
            throw new ArgumentException("Invalid customer document id!");
        }
        
        if(IsValidEmail(email)){
            throw new ArgumentException("Invalid customer e-mail!");
        };

        // Id = Math.Rand(1, 1000);
        Id = 1;
        Name = name;
        DocumentId = documentId;
        Address = address;
        Email = email;
    }

    public static async Task<IList<Customer>> GetAllCustomers(Context db){
        return await db.Customers.ToArrayAsync();
    }

    public static async Task<Customer> GetCustomer(int id, Context db){
        return await db.Customers.FindAsync(id)
            is Customer customer
                ? customer
                : null;
    }

    public static async Task<Customer> CreateCustomer(Customer customer, Context db){
        db.Customers.Add(customer);
        await db.SaveChangesAsync();

        return customer;
    }

    public static async Task UpdateCustomer(int id, Customer inputCustomer, Context db){
        var Customer = await db.Customers.FindAsync(id);

        // if (Customer is not null) {
        //     Customer.Buyer.Name = inputCustomer.Buyer.Name;
        //     customer.Status = inputcustomer.Status;

            await db.SaveChangesAsync(); 
    }

    public static async Task DeleteCustomer(int id, Context db){
        if (await db.Customers.FindAsync(id) is Customer customer)
        {
            db.Customers.Remove(customer);
            await db.SaveChangesAsync();
        }
    }

    private bool IsValidDocumentId(string documentId)
    {
        return documentId.Length == 11;
    }

    private bool IsEmptyOrNull(string info){
        // return string.IsEmptyOrNull(info);
        return false;
    }

    private bool IsValidEmail(string email)
    {
        return email.Contains("@") && email.Length > 5;
    }
}
