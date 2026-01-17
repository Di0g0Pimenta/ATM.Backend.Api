using ATM.Backend.Api.Models;
using ATM.Backend.Api.Models.DbConnection;

namespace ATM.Backend.Api.Repositories;

public class ClientDao : GenericDao<Client>
{
    public ClientDao(AppDbContext context) : base(context)
    {
    }
    
    /// <summary>
    /// Verifica se um username já existe no banco de dados.
    /// </summary>
    /// <param name="username">Username a ser verificado</param>
    /// <returns>True se o username já existe, False caso contrário</returns>
    public bool UsernameExists(string username)
    {
        return _context.Set<Client>()
            .Any(c => c.Username.ToLower() == username.ToLower());
    }
}