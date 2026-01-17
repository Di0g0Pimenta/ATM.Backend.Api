using System.ComponentModel.DataAnnotations;

namespace ATM.Backend.Api.Dto;

public class NewClientDto
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Username { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 4)]
    public string Password { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "BankId is required and must be valid.")]
    public int BankId { get; set; }
    
    [Required]
    [StringLength(12)]
    public string cardNumber {get; set; }
}