namespace ATM.Backend.Api.Dto;

public class TransactionDto
{
    public int scrId { get; set; }
    public string dstCardNumber { get; set; }
    public decimal amount { get; set; }
}