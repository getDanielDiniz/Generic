using Generic.Domain.Entities;
using Generic.Domain.Security.Authentication;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Generic.Infraestructure.Security.Authentication
{
    internal class TokenGenerator : ITokenGenerator
    {
        private readonly int _minutesToExpire;
        private readonly string _secretKey;
        public TokenGenerator(
            int minutesToExpire,
            string secretKey
        ){
            _minutesToExpire = minutesToExpire;
            _secretKey = secretKey;
        }

        public string GenerateToken(UserEntity user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = SignInKeyEncoding();
            var claims = new List<Claim>
            {
                new (ClaimTypes.Email, user.Email),
                new (ClaimTypes.Sid, user.SecurityId.ToString()),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_minutesToExpire),
                SigningCredentials = new SigningCredentials(
                    securityKey,
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private SymmetricSecurityKey SignInKeyEncoding()
        {
            var base64Key = Convert.FromBase64String(_secretKey);
            return new SymmetricSecurityKey(base64Key);
        }
    }
}
