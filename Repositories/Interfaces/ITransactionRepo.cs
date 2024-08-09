using Customer_Balance_Paltform.DTOS;

namespace Customer_Balance_Paltform.Repositories;

public interface ITransactionRepo
{
    Task<TransactionDto> CreateTransactionAsync(TransactionDto transaction);
    Task<IEnumerable<TransactionDto>> GetTransactionsByCustomerIdAsync(int customerId);
    Task<TransactionDto> GetTransactionByIdAsync(int id);
}