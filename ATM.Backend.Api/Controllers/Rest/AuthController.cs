using Microsoft.AspNetCore.Mvc;
using ATM.Backend.Api.Models;
using ATM.Backend.Api.Repositories;
using ATM.Backend.Api.Services;
using ATM.Backend.Api.Data;

namespace ATM.Backend.Api.Controllers.Rest
{
    /// <summary>
    /// Controller responsável pela autenticação de usuários.
    /// </summary>
    [Route("multibanco/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ClientRepository _clientRepository;
        private readonly ITokenService _tokenService;

        public AuthController(AppDbContext context, ITokenService tokenService)
        {
            _clientRepository = new ClientRepository(context);
            _tokenService = tokenService;
        }

        /// <summary>
        /// Endpoint para realizar o login e obter um token JWT.
        /// </summary>
        /// <param name="login">Modelo contendo email e senha.</param>
        /// <returns>Dados do cliente e o token de acesso.</returns>
        [HttpPost("login")]
        public ActionResult<dynamic> Authenticate([FromBody] LoginModel login)
        {
            // Busca o cliente pelo email e senha (idealmente a senha deveria estar hasheada)
            var clients = _clientRepository.ListAll();
            var client = clients.FirstOrDefault(x => x.Email == login.Email && x.Password == login.Password);

            if (client == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            // Gera o token JWT para o cliente encontrado
            var token = _tokenService.GenerateToken(client);

            return new
            {
                client = new { id = client.Id, name = client.Name, email = client.Email },
                token = token
            };
        }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
