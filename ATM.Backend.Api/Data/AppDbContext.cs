using Microsoft.EntityFrameworkCore;
using ATM.Backend.Api.Models;

namespace ATM.Backend.Api.Data;

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
    public DbSet<Movement> Movements { get; set; }
}