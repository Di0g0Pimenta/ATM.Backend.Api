using ATM.Backend.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ATM.Backend.Api.Models;
using ATM.Backend.Api.Models.DbConnection;
using ATM.Backend.Api.Repositories;
using ATM.Backend.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace ATM.Backend.Api.Controllers.Rest
{
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
        private readonly DeleteAccountService _deleteAccountService;

        public ClientController(AppDbContext context, DeleteAccountService deleteAccountService)
        {
            _clientDao = new ClientDao(context);
            _createAccountService = new CreateAccountService(context);
            _deleteAccountService = deleteAccountService;
        }

        // Retorna lista de todos Clients -- multibanco/client
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetAllClient()
        {
            return _clientDao.ListAll();
        }

        // Cria um novo cliente -- multibanco/client
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Client>> CreateClient(NewClientDto newClientDto)
        {
            _createAccountService.createNewClient(newClientDto);
            return Created("", null);
        }

        // Atualiza dados do cliente logado
        [HttpPut]
        public async Task<ActionResult<Client>> UpdateClient(UpdateClientDto updateClientDto)
        {
            var clientIdClaim = User.FindFirst("AccountId")?.Value;
            if (string.IsNullOrEmpty(clientIdClaim))
                return Unauthorized();

            int clientId = int.Parse(clientIdClaim);
            Client client = _clientDao.GetById(clientId);

            if (client == null)
                return NotFound(new { error = "Client not found." });

            if (BCrypt.Net.BCrypt.Verify(updateClientDto.Password, client.Password))
            {
                client.Password = BCrypt.Net.BCrypt.HashPassword(updateClientDto.NewPassword);
                _clientDao.Update(client);
                return NoContent();
            }

            return BadRequest(new { error = "Passwords do not match." });
        }

        // Deleta cliente logado
        [HttpDelete]
        public IActionResult DeleteClient()
        {
            var accountIdClaim = User.FindFirst("AccountId")?.Value;
            if (string.IsNullOrEmpty(accountIdClaim))
                return Unauthorized();

            int accountId = int.Parse(accountIdClaim);
            _deleteAccountService.DeleteAccount(accountId);

            return NoContent();
        }

        // Atualiza a imagem de perfil do cliente logado
        [HttpPost("image")]
        public async Task<ActionResult> UpdateClientImage([FromBody] UpdateClientImageDto imageDto)
        {
            var clientIdClaim = User.FindFirst("AccountId")?.Value;
            if (string.IsNullOrEmpty(clientIdClaim))
                return Unauthorized();

            int clientId = int.Parse(clientIdClaim);

            if (!imageDto.IsValid(out string errorMessage))
                return BadRequest(new { error = errorMessage });

            bool success = _clientDao.UpdateProfileImage(clientId, imageDto.ProfileImage);

            if (!success)
                return NotFound(new { error = "Client not found." });

            return Ok(new { message = "Profile image updated successfully." });
        }

        // Obtém a imagem de perfil do cliente logado
        [HttpGet("image")]
        public async Task<ActionResult> GetClientImage()
        {
            var clientIdClaim = User.FindFirst("AccountId")?.Value;
            if (string.IsNullOrEmpty(clientIdClaim))
                return Unauthorized();

            int clientId = int.Parse(clientIdClaim);

            string? profileImage = _clientDao.GetProfileImage(clientId);

            if (profileImage == null)
                return NotFound(new { error = "Client not found or no profile image set." });

            return Ok(new { profileImage });
        }

        // Remove a imagem de perfil do cliente logado
        [HttpDelete("image")]
        public async Task<ActionResult> DeleteClientImage()
        {
            var clientIdClaim = User.FindFirst("AccountId")?.Value;
            if (string.IsNullOrEmpty(clientIdClaim))
                return Unauthorized();

            int clientId = int.Parse(clientIdClaim);

            bool success = _clientDao.DeleteProfileImage(clientId);

            if (!success)
                return NotFound(new { error = "Client not found." });

            return NoContent();
        }

        // Atualiza a password do cliente logado
        [HttpPut("password")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordDto dto)
        {
            var userIdClaim = User.FindFirst("AccountId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized();

            int userId = int.Parse(userIdClaim);

            var client = _clientDao.GetById(userId);
            if (client == null)
                return NotFound(new { error = "Client not found." });

            client.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            _clientDao.Update(client);

            return NoContent();
        }
    }
}
