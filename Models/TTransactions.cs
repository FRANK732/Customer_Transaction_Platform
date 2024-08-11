using System.Text.Json.Serialization;

namespace Customer_Balance_Paltform.Models;

public class TTransactions
{
    public int TransactionID { get; set; } // PKey

    public int CustomerID { get; set; } // FKey 

    public DateTime TransactionDate { get; set; }= DateTime.Now;

    public string TransactionType { get; set; } 

    public string UniqueNumber { get; set; } = RandomNumber(10000, 99999).ToString();

    public decimal Amount { get; set; }

    public decimal? Debit { get; set; }

    public decimal? Credit { get; set; }

    public decimal Balance { get; set; } 
    public string Remarks { get; set; } 
    

    // Customer
    [JsonIgnore]
    public TCustomer Customer { get; set; }
    
    static int RandomNumber(int min, int max)
    {
        Random random = new Random(); return random.Next(min, max);

    }
}
public enum TransactionType
{
    Invoice,
    Payment
}