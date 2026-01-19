using System.ComponentModel.DataAnnotations;

namespace ATM.Backend.Api.Dto;

public class TransactionDto
{
    [Required]
    public int scrId { get; set; }

    //[Required]
    [StringLength(20)]
    public string dstCardNumber { get; set; }

    [Required]
    [Range(0.01, 1000000, ErrorMessage = "Amount must be between 0.01 and 1,000,000.")]
    public decimal amount { get; set; }

    public int Entity { get; set; } = 0;  // Padrão 0 (não usado)
    public string? Reference { get; set; }  // Padrão null (não usado)

}