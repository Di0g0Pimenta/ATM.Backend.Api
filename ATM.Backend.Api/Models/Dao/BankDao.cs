using ATM.Backend.Api.Models;
using ATM.Backend.Api.Models.DbConnection;

namespace ATM.Backend.Api.Repositories;

public class BankDao : GenericDao<Bank>
{
    public BankDao(AppDbContext context) : base(context)
    {
    }
}