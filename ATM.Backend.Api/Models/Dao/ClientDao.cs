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
        return _context.Clients.Any(c => c.Username == username);
    }
    
    /// <summary>
    /// Atualiza a imagem de perfil de um cliente.
    /// </summary>
    /// <param name="clientId">ID do cliente</param>
    /// <param name="imageBase64">Imagem em formato Base64</param>
    /// <returns>True se atualizado com sucesso, False se cliente não encontrado</returns>
    public bool UpdateProfileImage(int clientId, string imageBase64)
    {
        var client = GetById(clientId);
        if (client == null)
        {
            return false;
        }
        
        client.ProfileImage = imageBase64;
        _context.SaveChanges();
        return true;
    }
    
    /// <summary>
    /// Obtém a imagem de perfil de um cliente.
    /// </summary>
    /// <param name="clientId">ID do cliente</param>
    /// <returns>Imagem em Base64 ou null se não existir</returns>
    public string? GetProfileImage(int clientId)
    {
        var client = GetById(clientId);
        return client?.ProfileImage;
    }
    
    /// <summary>
    /// Remove a imagem de perfil de um cliente.
    /// </summary>
    /// <param name="clientId">ID do cliente</param>
    /// <returns>True se removido com sucesso, False se cliente não encontrado</returns>
    public bool DeleteProfileImage(int clientId)
    {
        var client = GetById(clientId);
        if (client == null)
        {
            return false;
        }
        
        client.ProfileImage = null;
        _context.SaveChanges();
        return true;
    }
}