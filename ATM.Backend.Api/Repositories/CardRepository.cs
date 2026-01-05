using ATM.Backend.Api.Data;
using ATM.Backend.Api.Models;

namespace ATM.Backend.Api.Repositories;

public class CardRepository : GenericRepository<Card>
{
    public CardRepository(AppDbContext context) : base(context)
    {
        
    }
}