using ATM.Backend.Api.Models;
using ATM.Backend.Api.Models.DbConnection;
using Microsoft.EntityFrameworkCore;

namespace ATM.Backend.Api.Models.DbConnection
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            // Verifica se já existem clientes. Se sim, não faz nada.
            if (context.Clients.Any())
            {
                return;
            }

            // 1. Criar Banco padrão
            var bank = new Bank
            {
                Name = "Banco Antigravity",
                Code = "0001"
            };
            context.Banks.Add(bank);

            // 2. Criar Cliente padrão
            var client = new Client
            {
                Username = "admin",
                Password = BCrypt.Net.BCrypt.HashPassword("admin123")
            };
            context.Clients.Add(client);

            // 3. Criar Conta associada ao cliente
            var account = new Account
            {
                TotalBalance = 1000.00m,
                Client = client
            };
            context.Accounts.Add(account);

            // 4. Criar Cartão associado à conta e banco
            // Usamos o construtor existente que gera o número do cartão automaticamente
            var card = new Card(bank, account)
            {
                Balance = 1000.00m
            };
            context.Cards.Add(card);

            context.SaveChanges();
        }
    }
}
