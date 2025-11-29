using Generic.Domain.Security.Authentication;

namespace Generic.API.Services.Authentication
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Get()
        {
            _httpContextAccessor
                .HttpContext!
                .Request
                .Headers
                .TryGetValue("Authorization", out var authorization);

            var token = authorization.ToString();

            return token["Bearer ".Length..].Trim();
        }
    }
}
