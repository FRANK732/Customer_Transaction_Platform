namespace Customer_Balance_Paltform.DTOS;

public class CustomerDto
{
    public int CustomerID { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }

    public ContactInfoDto ContactInfo { get; set; }

    public decimal CurrentBalance { get; set; }
}

public class ContactInfoDto
{
    public string Email { get; set; }

    public string Phone { get; set; }
}


public class TransactionDto
{
    public string UniqueNumber { get; set; }
    
    public DateTime TransactionDate { get; set; }
    
    public string Remarks { get; set; }
    
    public decimal? Debit { get; set; }

    public decimal? Credit { get; set; }

    public decimal Balance { get; set; }
    public string TransactionType { get; set; }
}
