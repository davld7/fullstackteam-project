using Ioon.Domain.Common.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ioon.Application.Services
{
    public class JwtService : IJwtService
    {
        protected readonly string _secretKey;
        protected readonly string _issuer;
        protected readonly string _audience;

        /// <summary>
        /// Constructor del servicio JwtService.
        /// </summary>
        /// <param name="configuration">Configuración de la aplicación, incluyendo la configuración del token JWT.</param>
        public JwtService(IConfiguration configuration)
        {
            // Obtiene la clave secreta, el emisor y la audiencia desde la configuración.
            _secretKey = configuration["JWT:Key"]!;
            _issuer = configuration["JWT:Issuer"]!;
            _audience = configuration["JWT:Audience"]!;
        }

        /// <summary>
        /// Genera un token JWT para el usuario con la información proporcionada.
        /// </summary>
        /// <param name="userId">Identificador único del usuario.</param>
        /// <param name="username">Nombre de usuario del usuario.</param>
        /// <param name="email">Correo electrónico del usuario.</param>
        /// <returns>Token JWT generado.</returns>
        public string GenerateToken(string userId, string username, string email)
        {
            // Crea un manejador de tokens JWT.
            var tokenHandler = new JwtSecurityTokenHandler();

            // Convierte la clave secreta a un formato adecuado.
            var key = Encoding.UTF8.GetBytes(_secretKey);

            // Define los claims (afirmaciones) del token, incluyendo el identificador de nombre, el nombre de usuario, el correo electrónico y la audiencia.
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Email, email),
                new Claim(JwtRegisteredClaimNames.Aud, _audience),
            };

            // Configura el descriptor del token, estableciendo el sujeto, la expiración, las credenciales de firma y otros detalles.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _issuer,
                Audience = _audience,
            };

            // Crea el token utilizando el descriptor.
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Genera la representación en cadena del token.
            return tokenHandler.WriteToken(token);
        }
    }
}
