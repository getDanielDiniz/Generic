using Generic.Domain.Entities;

namespace Generic.Domain.Security.Authentication
{
    public interface ITokenGenerator
    {
        string GenerateToken(UserEntity user);
    }
}
