using Microsoft.EntityFrameworkCore;

namespace ATM.Backend.Api.Models;


[PrimaryKey(nameof(Id))]
public class Card : Model
{
    public int Id { get; set; }
    public int Balance { get; set; }
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
    
    public void Debit(int amount)
    {
        if (CanDebit(amount)) Balance -= amount;
    }

    public void Deposit(int amount)
    {
        if (CanDeposit(amount)) Balance += amount;
    }

    // Verifica se pode tirar ou depositar dinheiro

    private bool CanDeposit(int quantia)
    {
        return quantia > 0;
    }

    private bool CanDebit(int amount)
    {
        return amount < Balance && amount > 0;
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