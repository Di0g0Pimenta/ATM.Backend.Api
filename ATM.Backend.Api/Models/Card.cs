using Microsoft.EntityFrameworkCore;

namespace ATM.Backend.Api.Models;


[PrimaryKey(nameof(Id))]
public class Card : Model
{
    public int Id { get; set; }
    public int Balance { get; set; }
    public int Number { get; set; }
    public Bank Bank { get; set; }
    
    
    
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
}