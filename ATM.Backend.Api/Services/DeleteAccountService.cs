using ATM.Backend.Api.Models.DbConnection;
using ATM.Backend.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ATM.Backend.Api.Services
{
    public class DeleteAccountService
    {
        private readonly AppDbContext _context;

        public DeleteAccountService(AppDbContext context)
        {
            _context = context;
        }

        public void DeleteAccount(int accountId)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                // Buscar cartões da conta
                var cards = _context.Cards
                    .Where(c => c.Account.Id == accountId)
                    .Select(c => c.Id)
                    .ToList();

                // Apagar CardHistory
                var cardHistory = _context.CardHistory
                    .Where(h => cards.Contains(h.Card.Id));
                _context.CardHistory.RemoveRange(cardHistory);

                // Apagar Transactions
                var transactions = _context.Transactions
                    .Where(t =>
                        (t.SorceCard != null && cards.Contains(t.SorceCard.Id)) ||
                        (t.DestinyCard != null && cards.Contains(t.DestinyCard.Id))
                    );
                _context.Transactions.RemoveRange(transactions);

                // Apagar Cards
                var cardsEntities = _context.Cards
                    .Where(c => cards.Contains(c.Id));
                _context.Cards.RemoveRange(cardsEntities);

                // Apagar Account
                var account = _context.Accounts
                    .Include(a => a.Client)
                    .FirstOrDefault(a => a.Id == accountId);

                if (account != null)
                {
                    _context.Accounts.Remove(account);
                    
                    // Apagar Client
                    if (account.Client != null)
                        _context.Clients.Remove(account.Client);
                }

                _context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
