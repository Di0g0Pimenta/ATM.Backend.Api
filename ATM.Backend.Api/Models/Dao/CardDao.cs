using ATM.Backend.Api.Models;
using ATM.Backend.Api.Models.DbConnection;
using Microsoft.EntityFrameworkCore;

namespace ATM.Backend.Api.Repositories;

public class CardDao : GenericDao<Card>
{
    public CardDao(AppDbContext context) : base(context)
    {
        
    }

    public new Card GetById(int id)
    {
        return _context.Cards.Include(c => c.Account).FirstOrDefault(c => c.Id == id)!;
    }
    
    public Card GetByCardNum(string cardNum)
    {
        return _context.Cards.Include(c => c.Account).FirstOrDefault(c => c.Number == cardNum)!;
    }
}