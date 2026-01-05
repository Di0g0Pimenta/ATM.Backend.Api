using ATM.Backend.Api.Data;
using ATM.Backend.Api.Models;

namespace ATM.Backend.Api.Repositories;

public class ClientRepository : GenericRepository<Client>
{
    public ClientRepository(AppDbContext context) : base(context)
    {
    }
}