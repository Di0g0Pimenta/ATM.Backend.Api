using ATM.Backend.Api.Models;
using ATM.Backend.Api.Models.DbConnection;
using Microsoft.EntityFrameworkCore;

namespace ATM.Backend.Api.Repositories;

public class TransactionDao : GenericDao<Transaction>
{
    public TransactionDao(AppDbContext context) : base(context)
    {

        
    }
    
    public List<Transaction> ListTransactionByAccountId(int accountId)
    {
        var accountTransactions = _context.Transactions
            // (Optional) Include related data if you need to display it
            .Include(t => t.SorceCard)
            .Include(t => t.DestinyCard)
            // The Core Logic
            .Where(t => 
                (t.SorceCard.Account.Id == accountId) || 
                (t.DestinyCard.Account.Id == accountId)
            )
            .OrderByDescending(t => t.Date) // Usually you want the newest first
            .ToList();

        return accountTransactions;
    }
}