using ATM.Backend.Api.Models;

namespace ATM.Backend.Api.Dto;

public class NewClientDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public int BankId { get; set; }
}