using Customer_Balance_Paltform.DTOS;
using Customer_Balance_Paltform.Models;

namespace Customer_Balance_Paltform.Repositories;

public interface ICustomerRepo
{
    Task<TCustomer?> CreateCustomerAsync(TCustomer? customer);
    Task<TCustomer?> GetCustomerByIdAsync(int id);
    Task<IEnumerable<TCustomer>> GetAllCustomersAsync();
    Task<bool> UpdateCustomerAsync(TCustomer customer);
    Task<bool> DeleteCustomerAsync(int id);

}