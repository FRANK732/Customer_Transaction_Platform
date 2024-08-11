using System.Text.Json.Serialization;

namespace Customer_Balance_Paltform.Models;

public class TCustomer
{
    public int CustomerID { get; set; } 

    public string Name { get; set; } 

    public string Description { get; set; } 

    public TContactInfo ContactInfo { get; set; } 

    public decimal CurrentBalance { get; set; } 

    // Transactions
    [JsonIgnore]
    public ICollection<TTransactions> Transactions { get; set; }
}

public class TContactInfo
{
    public int ContactId { get; set; } //PKey
    public int CustomerId { get; set; } // FKey
    public string Email { get; set; }
    public string Phone { get; set; }
    public TCustomer Customer { get; set; }
}