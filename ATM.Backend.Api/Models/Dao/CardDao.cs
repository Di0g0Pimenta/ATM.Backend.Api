using ATM.Backend.Api.Models;
using ATM.Backend.Api.Models.DbConnection;
using Microsoft.EntityFrameworkCore;

namespace ATM.Backend.Api.Repositories;

public class CardDao : GenericDao<Card>
{
    public CardDao(AppDbContext context) : base(context)
    {
        
    }

    public bool CardNumberExists(string cardNum)
    {
        return _context.Cards.Any(c => c.Number == cardNum);
    }
    
    public new Card GetById(int id)
    {
        return _context.Cards
            .Include(c => c.Account)
            .Include(c => c.Bank)
            .FirstOrDefault(c => c.Id == id)!;
    }
    
    public Card GetByCardNum(string cardNum)
    {
        return _context.Cards
            .Include(c => c.Account)
            .Include(c => c.Bank)
            .FirstOrDefault(c => c.Number == cardNum)!;
    }

    public List<Card> ListAll(int accountId)
    {
        return _context.Cards
            .Include(c => c.Bank)
            .Where(c => c.Account.Id == accountId).ToList();
    }
}