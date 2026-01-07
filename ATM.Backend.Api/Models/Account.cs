using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ATM.Backend.Api.Models

{
    [PrimaryKey(nameof(Id))]
    public class Account : Model
    {
        public int Id { get; set; }
        public double Balance { get; set; } = 0;
        public string IBAN { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int BankId { get; set; }
        public Bank Bank { get; set; }


        // Deposita ou debita dinheiro da conta

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
}
