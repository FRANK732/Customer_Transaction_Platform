using Customer_Balance_Paltform.CBP.Infrastractures;
using Customer_Balance_Paltform.DTOS;
using Customer_Balance_Paltform.Models;

namespace Customer_Balance_Paltform.Repositories;

public class CustomerRepo : ICustomerRepo
{
    private readonly DbCTPContest _context;

    public CustomerRepo(DbCTPContest context)
    {
        _context = context;
    }
    
    public async Task<TCustomer> CreateCustomerAsync(TCustomer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return customer;
    }   

    public async Task<TCustomer> GetCustomerByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TCustomer>> GetAllCustomersAsync()
    {
        throw new NotImplementedException();
    }

    public async Task UpdateCustomerAsync(TCustomer customer)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteCustomerAsync(int id)
    {
        throw new NotImplementedException();
    }
}