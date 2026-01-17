using ATM.Backend.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ATM.Backend.Api.Models;
using ATM.Backend.Api.Models.DbConnection;
using ATM.Backend.Api.Repositories;
using ATM.Backend.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace ATM.Backend.Api.Controllers.Rest;

/// <summary>
/// Controller para gestão de clientes. Requer autenticação.
/// </summary>
[Route("multibanco/client")]
[ApiController]
[Authorize]
public class ClientController : ControllerBase
{
    private readonly ClientDao _clientDao;
    private readonly CreateAccountService _createAccountService;
    
    public ClientController(AppDbContext context)
    {
        _clientDao = new ClientDao(context);
        _createAccountService = new CreateAccountService(context);
    }
    
    
    // Retorna lista de todos Clients -- multibanco/client
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Client>>> GetAllClient()
    {
        return _clientDao.ListAll();
    }

    // Retorna Client por id ou 404 se nao achar -- multibanco/client/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Client>> GetClient(int id)
    {
        Client client = _clientDao.GetById(id);

        if (client == null)
        {
            return NotFound();
        }
        
        return client;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<Client>> CreateClient(NewClientDto newClientDto)
    {
        // A exceção InvalidOperationException será capturada pelo GlobalExceptionMiddleware
        // e retornará automaticamente um BadRequest (400) com a mensagem de erro
        _createAccountService.createNewClient(newClientDto);
        
        return Created();
    }
    
    // Atualiza um Client -- multibanco/client/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult<Client>> UpdateClient(int id, Client client)
    {
        if (id != client.Id)
        {
            return BadRequest();
        }
        
        if (_clientDao.Update(client) == null)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    // Deleta um Client por id, retorna 404 se nao achar -- multibanco/client/{id}
    [HttpDelete]
    public async Task<ActionResult<Client>> DeleteClient(int id)
    {
        Client client = _clientDao.Delete(id);

        if (client == null)
        {
            return NotFound();
        }
        
        return client;
    }
}