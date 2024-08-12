using Customer_Balance_Paltform.CBP.Infrastractures;
using Customer_Balance_Paltform.DTOS;
using Customer_Balance_Paltform.Models;
using Customer_Balance_Paltform.Models.RequestModel;
using Microsoft.EntityFrameworkCore;

namespace Customer_Balance_Paltform.Repositories;

public class CustomerRepo : ICustomerRepo
{
    private readonly DbCTPContest _context;

    public CustomerRepo(DbCTPContest context)
    {
        _context = context;
    }
    
    public async Task<TCustomer?> CreateCustomerAsync(TCustomer? customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return customer;
    }   

    public async Task<TCustomer?> GetCustomerByIdAsync(int id)
    {
        return await _context.Customers.Include(c => c.ContactInfo)
            .Include(c => c.Transactions)
            .FirstOrDefaultAsync(c => c.CustomerID == id);
    }

    public async Task<IEnumerable<TCustomer>> GetAllCustomersAsync() => await _context.Customers
            .Include(c => c.ContactInfo)
            .Include(c => c.Transactions)
            .ToListAsync();

    public async Task<bool> UpdateCustomerAsync(int customerId, TCustomer customer)
    {
        var existingCustomer = await _context.Customers.FindAsync(customerId);

        var existingContact = await _context.ContactInfos.FindAsync(customerId);

        if (existingCustomer == null)
        {
            return false;
        }

        existingCustomer.Name = customer.Name;
        existingCustomer.Description = customer.Description;
        existingContact.Email = customer.ContactInfo.Email;
        existingContact.Phone = customer.ContactInfo.Phone;

        await _context.SaveChangesAsync();

        return true;
    }



    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

   

}