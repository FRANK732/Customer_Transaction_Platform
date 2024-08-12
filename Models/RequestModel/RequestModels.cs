using System.ComponentModel.DataAnnotations;

namespace Customer_Balance_Paltform.Models.RequestModel;

public class Customer
{
    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; }

    public string? Description { get; set; }

    public ContactInfo ContactInfo { get; set; }
    [Required] public decimal CurrentBalance { get; set; } = 0;
}

public class UpdateCustomer
{
    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; }

    public string? Description { get; set; }

    public ContactInfo ContactInfo { get; set; }
}

public class ContactInfo
{
    [Required(AllowEmptyStrings = false)]
    public string Email { get; set; }
    
    [Required(AllowEmptyStrings = false)]
    public string Phone { get; set; }
}

public class Transaction
{
    public int CustomerId { get; set; }
    public string? Remarks { get; set; }
    public DateTime? TransactionDate { get; set; } = null;
    public TransactionType TransactionType { get; set; }

    public decimal Amount { get; set; }
}

