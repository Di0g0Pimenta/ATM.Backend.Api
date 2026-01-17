using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ATM.Backend.Api.Models;


[PrimaryKey(nameof(Id))]
public class Card : Model
{
    public int Id { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Balance { get; set; }
    public string Number { get; set; }
    public Bank Bank { get; set; }
    public Account Account { get; set; }

    public Card()
    {
        
    }
    
    public Card(Bank bank, Account account, string number)
    {
        Account = account;
        Bank = bank;
        Balance = 0;
        Number = number;
    }
    
    
    
    // Tira ou coloca dinheiro no cartao
    
    public void Withdraw(decimal amount)
    { 
        Balance -= amount;
    }

    public void Deposit(decimal amount)
    { 
        Balance += amount;
    }
}