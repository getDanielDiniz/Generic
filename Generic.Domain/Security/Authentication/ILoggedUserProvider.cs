using Generic.Domain.Entities;

namespace Generic.Domain.Security.Authentication
{
    public interface ILoggedUserProvider
    {
        Task<UserEntity> Get();
    }
}
