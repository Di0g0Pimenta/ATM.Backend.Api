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
        string GenerateToken(Client client, int accountId);
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
        public string GenerateToken(Client client, int accountId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, client.Username),
                    new Claim(ClaimTypes.NameIdentifier, client.Id.ToString()),
                    new Claim("AccountId", accountId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
