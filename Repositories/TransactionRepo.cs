using AutoMapper;
using Customer_Balance_Paltform.CBP.Infrastractures;
using Customer_Balance_Paltform.DTOS;
using Customer_Balance_Paltform.Models;
using Microsoft.EntityFrameworkCore;
using TransactionType = Customer_Balance_Paltform.Models.TransactionType;

namespace Customer_Balance_Paltform.Repositories;

public class TransactionRepo : ITransactionRepo
{
    private readonly DbCTPContest _context;
    private readonly IMapper _mapper;

    public TransactionRepo(DbCTPContest contest,IMapper mapper)
    {
        _context = contest;
        _mapper = mapper;
    }
    public async Task<TTransactions?> CreateTransactionAsync(TTransactions? transaction)
    {
        _context.Transactions.Add(transaction);

        await _context.SaveChangesAsync();

        await RecalculateBalances(transaction.CustomerID);

        return transaction;

    }
    

    public async Task<IEnumerable<TTransactions>> GetTransactionsByCustomerIdAsync(int customerId)
    {
        return await _context.Transactions
            .Where(t => t.CustomerID == customerId)
            .OrderBy(t => t.TransactionDate)
            .ToListAsync();
    }

    public async Task<TransactionDto?> GetTransactionByIdAsync(string id)
    {
        var transaction = await _context.Transactions
            .Where(d => d.UniqueNumber == id)
            .FirstOrDefaultAsync();

        if (transaction == null)
        {
            return null;
        }

        var transactionDto = _mapper.Map<TransactionDto>(transaction);

        return transactionDto;
    }

    public async Task<TransactionReport> GenerateTransactionReportAsync(int? customerId, DateTime? startDate, DateTime? endDate ) 
    {
        var transactionsQuery = _context.Transactions.AsQueryable();    

        transactionsQuery = transactionsQuery.Where(t => t.CustomerID == customerId);


        // Applying date range filtering
        if (startDate.HasValue)
            transactionsQuery = transactionsQuery.Where(t => t.TransactionDate >= startDate.Value);
        

        if (endDate.HasValue)
            transactionsQuery = transactionsQuery.Where(t => t.TransactionDate <= endDate.Value);
        

        if (!startDate.HasValue && !endDate.HasValue) 
            transactionsQuery = transactionsQuery.Where(t => t.TransactionDate <= DateTime.Now);

        // Order by date
        var transactions = await transactionsQuery.OrderBy(t => t.TransactionDate).ToListAsync();

        var finalTransaction = _mapper.Map<List<TransactionDto>>(transactions);

        var report = new TransactionReport
        {
            Transactions = finalTransaction
        };

     
        return report;
    }


    private async Task RecalculateBalances(int customerId)
    {
        var transactions = await _context.Transactions
            .Where(t => t.CustomerID == customerId)
            .OrderBy(t => t.TransactionDate)
            .ToListAsync();

        var customer = await _context.Customers
            .Where(d => d.CustomerID == customerId)
            .FirstOrDefaultAsync();

        if (customer != null)
        {
            decimal balance = 0;

            foreach (var transaction in transactions)
            {
                if (transaction.TransactionType == TransactionType.Invoice.ToString())
                {
                    balance += transaction.Amount; // Debit (Increase balance)
                    transaction.Debit = transaction.Amount;
                    transaction.Credit = 0;
                }
                else if (transaction.TransactionType == TransactionType.Payment.ToString())
                {
                    balance -= transaction.Amount; // Credit (Decrease balance)
                    transaction.Credit = transaction.Amount;
                    transaction.Debit = 0;
                }

                transaction.Balance = balance;
            }

            customer.CurrentBalance += balance;

            _context.Transactions.UpdateRange(transactions);
            
            Console.WriteLine($"Final calculated balance for customer {customerId}: {balance}");
             _context.Customers.UpdateRange(customer);

            await _context.SaveChangesAsync();
        }
    }

}