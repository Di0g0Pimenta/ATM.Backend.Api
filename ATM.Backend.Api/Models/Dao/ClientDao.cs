using ATM.Backend.Api.Models;
using ATM.Backend.Api.Models.DbConnection;

namespace ATM.Backend.Api.Repositories;

public class ClientDao : GenericDao<Client>
{
    public ClientDao(AppDbContext context) : base(context)
    {
    }
}