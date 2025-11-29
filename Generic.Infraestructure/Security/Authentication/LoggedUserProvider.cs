using Generic.Domain.Entities;
using Generic.Domain.Security.Authentication;
using Generic.Infraestructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Generic.Infraestructure.Security.Authentication
{
    public class LoggedUserProvider : ILoggedUserProvider
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly GenericDbContext _context;
        public LoggedUserProvider(ITokenProvider tokenProvider, GenericDbContext context)
        {
            _context = context;
            _tokenProvider = tokenProvider;
        }
        public async Task<UserEntity> Get()
        {
            var token = _tokenProvider.Get();
            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtToken = tokenHandler.ReadJwtToken(token);

            var userId = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

            var user = await _context.Users
                .AsNoTracking()
                .FirstAsync(user => user.SecurityId == Guid.Parse(userId));

            return user;

        }
    }
}
