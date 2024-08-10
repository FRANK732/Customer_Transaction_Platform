using Customer_Balance_Paltform.DTOS;
using Customer_Balance_Paltform.Models;

namespace Customer_Balance_Paltform.Repositories;

public interface ITransactionRepo
{
    Task<TTransactions?> CreateTransactionAsync(TTransactions? transaction);
    Task<IEnumerable<TTransactions>> GetTransactionsByCustomerIdAsync(int customerId);
    Task<TransactionDto?> GetTransactionByIdAsync(string id);
    Task<TransactionReport> GenerateTransactionReportAsync(int? customerId, DateTime? startDate, DateTime? endDate);

}