using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ATM.Backend.Api.Models;
using Microsoft.IdentityModel.Tokens;

namespace ATM.Backend.Api.Services
{
    /// <summary>
    /// Interface para o serviço de geração de tokens JWT.
    /// </summary>
    public interface ITokenService
    {
        string GenerateToken(Client client);
    }

    /// <summary>
    /// Implementação do serviço de tokens utilizando JWT.
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Gera um token JWT para um cliente autenticado.
        /// </summary>
        /// <param name="client">O cliente para o qual o token será gerado.</param>
        /// <returns>Uma string contendo o token JWT.</returns>
        public string GenerateToken(Client client)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]); // Obtém a chave secreta da configuração
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Define as informações contidas no token (Claims)
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, client.Name),
                    new Claim(ClaimTypes.Email, client.Username),
                    new Claim(ClaimTypes.NameIdentifier, client.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2), // Token expira em 2 horas
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
