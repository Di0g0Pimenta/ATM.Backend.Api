using Microsoft.EntityFrameworkCore;

namespace ATM.Backend.Api.Models.DbConnection;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    
    
    // Tabelas do banco de dados
    public DbSet<Client> Clients { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<Bank> Banks { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Services> Services { get; set; }
    public DbSet<CardHistory> CardHistory { get; set; }
}