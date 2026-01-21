using ATM.Backend.Api.Models;
using ATM.Backend.Api.Models.DbConnection;
using Microsoft.EntityFrameworkCore;

namespace ATM.Backend.Api.Repositories;

public class CardHistoryDao : GenericDao<CardHistory>
{
    public CardHistoryDao(AppDbContext context) : base(context)
    {
    }
    
    public List<CardHistory> ListAllByCardId(int cardId)
    {
        return _context.CardHistory
            .Where(c => c.Card.Id == cardId).ToList();
    }
}