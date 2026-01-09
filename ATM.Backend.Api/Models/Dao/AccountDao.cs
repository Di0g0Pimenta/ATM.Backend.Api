using ATM.Backend.Api.Models;
using ATM.Backend.Api.Models.DbConnection;

namespace ATM.Backend.Api.Repositories;

public class AccountDao : GenericDao<Account>
{
    public AccountDao(AppDbContext context) : base(context)
    {
    }
}