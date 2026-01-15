using Microsoft.AspNetCore.Mvc;
using ATM.Backend.Api.Models;
using ATM.Backend.Api.Models.DbConnection;
using ATM.Backend.Api.Repositories;
using ATM.Backend.Api.Services;

namespace ATM.Backend.Api.Controllers.Rest
{
    /// <summary>
    /// Controller responsável pela autenticação de usuários.
    /// </summary>
    [Route("multibanco/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ClientDao _clientDao;
        private readonly ITokenService _tokenService;

        public AuthController(AppDbContext context, ITokenService tokenService)
        {
            _clientDao = new ClientDao(context);
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
            
            var clients = _clientDao.ListAll();
            var client = clients.FirstOrDefault(x => x.Username == login.Username && x.Password == login.Password);

            if (client == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            // Gera o token JWT para o cliente encontrado
            var token = _tokenService.GenerateToken(client);

            return new
            {
                client = new { id = client.Id, username = client.Username },
                token = token
            };
        }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
