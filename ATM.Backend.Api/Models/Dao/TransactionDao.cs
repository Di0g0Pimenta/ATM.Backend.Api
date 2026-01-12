using ATM.Backend.Api.Models;
using ATM.Backend.Api.Models.DbConnection;

namespace ATM.Backend.Api.Repositories;

public class TransactionDao : GenericDao<Transaction>
{
    public TransactionDao(AppDbContext context) : base(context)
    {
    }
}