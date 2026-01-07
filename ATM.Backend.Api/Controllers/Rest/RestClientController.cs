using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ATM.Backend.Api.Models;
using ATM.Backend.Api.Repositories;
using ATM.Backend.Api.Data;

using Microsoft.AspNetCore.Authorization;

namespace ATM.Backend.Api.Controllers.Rest;

/// <summary>
/// Controller para gestão de clientes. Requer autenticação.
/// </summary>
[Route("multibanco/client")]
[ApiController]
[Authorize]
public class RestClientController : ControllerBase
{
    public RestClientController(AppDbContext context)
    {
        _clientRepository = new ClientRepository(context);
    }

    private ClientRepository _clientRepository;
    
    
    // Retorna lista de todos Clients -- multibanco/client
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Client>>> GetAllClient()
    {
        return _clientRepository.ListAll();
    }

    // Retorna Client por id ou 404 se nao achar -- multibanco/client/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Client>> GetClient(int id)
    {
        Client client = _clientRepository.GetById(id);

        if (client == null)
        {
            return NotFound();
        }
        
        return client;
    }

    // Cria um Client -- multibanco/client
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<Client>> CreateClient(Client client)
    {
        _clientRepository.Create(client);
        
        return CreatedAtAction(nameof(GetClient), new { id = client.Id }, client);
    } 
    
    // Atualiza um Client -- multibanco/client/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult<Client>> UpdateClient(int id, Client client)
    {
        if (id != client.Id)
        {
            return BadRequest();
        }
        
        if (_clientRepository.Update(client) == null)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    // Deleta um Client por id, retorna 404 se nao achar -- multibanco/client/{id}
    [HttpDelete]
    public async Task<ActionResult<Client>> DeleteClient(int id)
    {
        Client client = _clientRepository.Delete(id);

        if (client == null)
        {
            return NotFound();
        }
        
        return client;
    }
}