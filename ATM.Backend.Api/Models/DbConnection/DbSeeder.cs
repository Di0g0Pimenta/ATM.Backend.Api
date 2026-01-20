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
            Password = BCrypt.Net.BCrypt.HashPassword("admin123"),
            // Imagem de perfil padrão para todos os clientes
            ProfileImage = "data:image/png;base64,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"
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
            var card = new Card(bank, account, "111111111111")
            {
                Balance = 1000.00m
            };
            context.Cards.Add(card);

            context.SaveChanges();
        }
    }
}
