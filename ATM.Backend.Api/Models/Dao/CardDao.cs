using ATM.Backend.Api.Models;
using ATM.Backend.Api.Models.DbConnection;

namespace ATM.Backend.Api.Repositories;

public class CardDao : GenericDao<Card>
{
    public CardDao(AppDbContext context) : base(context)
    {
        
    }
}