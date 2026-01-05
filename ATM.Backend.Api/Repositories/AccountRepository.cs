using ATM.Backend.Api.Data;
using ATM.Backend.Api.Models;

namespace ATM.Backend.Api.Repositories;

public class AccountRepository : GenericRepository<Account>
{
    public AccountRepository(AppDbContext context) : base(context)
    {
    }
}