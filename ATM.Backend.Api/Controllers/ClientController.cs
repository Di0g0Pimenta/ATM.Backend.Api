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
    [HttpPut()]
    public async Task<ActionResult<Client>> UpdateClient(UpdateClientDto updateClientDto)
    {
        var clientIdClaim = User.FindFirst("AccountId")?.Value;
        if (string.IsNullOrEmpty(clientIdClaim))
            return Unauthorized();

        int clientId = int.Parse(clientIdClaim);
        
        Client client = _clientDao.GetById(clientId);

        if (BCrypt.Net.BCrypt.Verify(updateClientDto.Password, client.Password))
        {
            client.Password = BCrypt.Net.BCrypt.HashPassword(updateClientDto.NewPassword);
            _clientDao.Update(client);
            return NoContent();
            
        }
        
        return BadRequest(new { error = "Passwords do not match." });
    }
    
    // Deleta um Client por id, retorna 404 se nao achar -- multibanco/client/{id}
    [HttpDelete]
    public async Task<ActionResult> DeleteClient()
    {
        var clientIdClaim = User.FindFirst("AccountId")?.Value;
        if (string.IsNullOrEmpty(clientIdClaim))
            return Unauthorized();
        
        int clientId = int.Parse(clientIdClaim);
        
        Client client = _clientDao.GetById(clientId);
        
        _clientDao.Delete(client.Id);
        
        return NoContent();
    }
    
    /// <summary>
    /// Upload ou atualiza a imagem de perfil de um cliente.
    /// </summary>
    /// <param name="id">ID do cliente</param>
    /// <param name="imageDto">DTO contendo a imagem em Base64</param>
    /// <returns>200 OK se sucesso, 400 se validação falhar, 404 se cliente não existir</returns>
    [HttpPost("{id}/image")]
    public async Task<ActionResult> UpdateClientImage(int id, [FromBody] UpdateClientImageDto imageDto)
    {
        // Valida o DTO
        if (!imageDto.IsValid(out string errorMessage))
        {
            return BadRequest(new { error = errorMessage });
        }
        
        // Atualiza a imagem
        bool success = _clientDao.UpdateProfileImage(id, imageDto.ProfileImage);
        
        if (!success)
        {
            return NotFound(new { error = "Client not found." });
        }
        
        return Ok(new { message = "Profile image updated successfully." });
    }
    
    /// <summary>
    /// Obtém a imagem de perfil de um cliente.
    /// </summary>
    /// <param name="id">ID do cliente</param>
    /// <returns>200 OK com imagem em Base64, 404 se cliente não existir ou não tiver imagem</returns>
    [HttpGet("{id}/image")]
    public async Task<ActionResult> GetClientImage(int id)
    {
        string? profileImage = _clientDao.GetProfileImage(id);
        
        if (profileImage == null)
        {
            return NotFound(new { error = "Client not found or no profile image set." });
        }
        
        return Ok(new { profileImage });
    }
    
    /// <summary>
    /// Remove a imagem de perfil de um cliente.
    /// </summary>
    /// <param name="id">ID do cliente</param>
    /// <returns>204 No Content se sucesso, 404 se cliente não existir</returns>
    [HttpDelete("{id}/image")]
    public async Task<ActionResult> DeleteClientImage(int id)
    {
        bool success = _clientDao.DeleteProfileImage(id);
        
        if (!success)
        {
            return NotFound(new { error = "Client not found." });
        }
        
        return NoContent();
    }

    /// <summary>
    /// Atualiza a password de um cliente.
    /// </summary>
    /// <param name="id">ID do cliente</param>
    /// <param name="dto">Nova password</param>
    /// <returns>204 No Content se sucesso</returns>
    [HttpPut("{id}/password")]
    public async Task<IActionResult> UpdatePassword(int id, UpdatePasswordDto dto)
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
            return Unauthorized();

        int userId = int.Parse(userIdClaim);

        // Impede mudar password de outro utilizador
        if (userId != id)
            return Forbid();

        var client = _clientDao.GetById(id);

        if (client == null)
            return NotFound(new { error = "Client not found." });

        client.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
        _clientDao.Update(client);

        return NoContent();
    }

}