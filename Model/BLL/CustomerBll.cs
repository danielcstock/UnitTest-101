namespace Model.BLL;

using Microsoft.EntityFrameworkCore;
using Model.Classes;

public class CustomerBll
{
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
        if(customer.Id <= 0){
            throw new ArgumentException("Invalid customer ID!");
        }
        
        if(String.IsNullOrEmpty(customer.Name)){
            throw new ArgumentException("Invalid customer name!");
        };

        if(String.IsNullOrEmpty(customer.Address)){
            throw new ArgumentException("Invalid customer address!");
        };
        
        if(!IsValidDocumentId(customer.DocumentId)){
            throw new ArgumentException("Invalid customer document id!");
        }
        
        if(!IsValidEmail(customer.Email)){
            throw new ArgumentException("Invalid customer e-mail!");
        };

        db.Customers.Add(customer);
        await db.SaveChangesAsync();

        return customer;
    }

    public static async Task UpdateCustomer(int id, Customer inputCustomer, Context db){
        var customer = await db.Customers.FindAsync(id);

        if (customer is not null) {
            customer.Name = inputCustomer.Name;
            customer.DocumentId = inputCustomer.DocumentId;
            customer.Address = inputCustomer.Address;
            customer.Email = inputCustomer.Email;

            await db.SaveChangesAsync(); 
        }
    }

    public static async Task DeleteCustomer(int id, Context db){
        if (await db.Customers.FindAsync(id) is Customer customer)
        {
            db.Customers.Remove(customer);
            await db.SaveChangesAsync();
        }
    }

    private static bool IsValidDocumentId(string documentId)
    {
        return documentId.Length == 11;
    }

    private static bool IsValidEmail(string email)
    {
        return email.Contains("@") && email.Length > 5;
    }
}
