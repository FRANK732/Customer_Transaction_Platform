namespace Customer_Balance_Paltform.Models;

public class TTransactions
{
    public int TransactionID { get; set; } // PKey

    public int CustomerID { get; set; } // FKey 

    public DateTime TransactionDate { get; set; } 

    public string TransactionType { get; set; } 

    public string UniqueNumber { get; set; } 

    public decimal Amount { get; set; } 

    public decimal? Debit { get; set; } 

    public decimal? Credit { get; set; } 

    public decimal Balance { get; set; } 

    // Customer
    public TCustomer Customer { get; set; }
}