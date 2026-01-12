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
    
    public Card(Bank bank, Account account)
    {
        Account = account;
        Bank = bank;
        Balance = 0;
        Number = createCardNumber();
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

    private string createCardNumber()
    {
        Random random = new Random();

        string number = Bank.Code;

        for (int i = 0; i < 12; i++)
        {
            number += random.Next(0, 9).ToString();
        }
        
        return number;
    }
    
}