using ATM.Backend.Api.Models;
using ATM.Backend.Api.Repositories;
using ATM.Backend.Api.Data;

namespace ATM.Backend.Api.Controllers.Local;

public class LocalClientController
{
    public LocalClientController(AppDbContext context)
    {
        _clientRepository = new ClientRepository(context);
    }
    
    private ClientRepository _clientRepository;

    // Retorna uma lista de todos clientes
    
    public List<Client> GetAllClient()
    {
        return _clientRepository.ListAll();
    }
    
    // Retorna um client por Id -- Retorna null se nao achar

    public Client GetClient(int id)
    {
        return _clientRepository.GetById(id);
    }
    
    // Cria um client
    
    public Client AddClient(Client client)
    {
        return _clientRepository.Create(client);
    }

    // Atualiza um Client
    
    public Client UpdateClient(Client client)
    {
        return _clientRepository.Update(client);
    }

    // Deleta um Client
    
    public Client DeleteClient(Client client)
    {
        return _clientRepository.Delete(client.Id);
    }
    
}