using System.ComponentModel.DataAnnotations;

namespace Customer_Balance_Paltform.Models;

public class ConnectionModel
{
    [Required]
    public string? Server { get; set; } 
}