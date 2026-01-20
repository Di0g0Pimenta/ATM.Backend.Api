using Microsoft.AspNetCore.Mvc;
using ATM.Backend.Api.Models;
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
        private readonly AccountDao _accountDao;
        private readonly ITokenService _tokenService;

        public AuthController(
            ClientDao clientDao,
            AccountDao accountDao,
            ITokenService tokenService)
        {
            _clientDao = clientDao;
            _accountDao = accountDao;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Endpoint para realizar o login e obter um token JWT.
        /// </summary>
        /// <param name="login">Modelo contendo username e senha.</param>
        /// <returns>Dados do cliente, accountId e token de acesso.</returns>
        [HttpPost("login")]
        public ActionResult<dynamic> Authenticate([FromBody] LoginModel login)
        {
            // Procurar cliente pelo username
            var client = _clientDao
                .ListAll()
                .FirstOrDefault(x => x.Username == login.Username);

            if (client == null || !BCrypt.Net.BCrypt.Verify(login.Password, client.Password))
            {
                return NotFound(new { message = "Usuário ou senha inválidos" });
            }

            // Obter a conta do cliente
            var account = _accountDao.ListAll().FirstOrDefault(a => a.Client.Id == client.Id);

            if (account == null)
                return NotFound(new { message = "Conta não encontrada" });

            // Gerar token incluindo accountId
            var token = _tokenService.GenerateToken(client, account.Id);

            // Retorno com accountId incluído
            return Ok(new
            {
                client = new { id = client.Id, username = client.Username },
                token = token
            });
        }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
