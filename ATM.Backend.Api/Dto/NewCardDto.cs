using System.ComponentModel.DataAnnotations;

namespace ATM.Backend.Api.Dto;

public class NewCardDto
{
    [Required]
    public int bankId { get; set; }
    
    [Required]
    public int accountId { get; set; }
    
    [Required]
    [StringLength(12,  MinimumLength = 12)]
    public string cardNumber { get; set; }
}