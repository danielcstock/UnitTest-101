namespace Model.BLL;

using Microsoft.EntityFrameworkCore;
using Model.Classes;

public class CustomerBll(IApplicationDbContext context)
{
    private readonly IApplicationDbContext _context = context;

    public async Task<IList<Customer>> GetAllCustomers(){
        return await _context.Customers.ToArrayAsync();
    }

    public async Task<Customer?> GetCustomer(int id){
        return await _context.Customers.FindAsync(id)
            is Customer customer
                ? customer
                : null;
    }

    public async Task<Customer> CreateCustomer(Customer customer){
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

        _context.Instance.Add(customer);
        await _context.Instance.SaveChangesAsync();

        return customer;
    }

    public async Task UpdateCustomer(int id, Customer inputCustomer){
        var customer = await _context.Customers.FindAsync(id);

        if (customer is not null) {
            customer.Name = inputCustomer.Name;
            customer.DocumentId = inputCustomer.DocumentId;
            customer.Address = inputCustomer.Address;
            customer.Email = inputCustomer.Email;

            await _context.Instance.SaveChangesAsync(); 
        }
    }

    public async Task DeleteCustomer(int id){
        var orderBll = new OrderBll(_context);
        var orders = await orderBll.GetAllOrdersByCustomerId(id);

        if(orders.Any()){
            throw new InvalidOperationException("Cannot delete customer. This customer is associated with one or more orders.");
        }

        if (await _context.Customers.FindAsync(id) is Customer customer)
        {
            _context.Customers.Remove(customer);
            await _context.Instance.SaveChangesAsync();
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
