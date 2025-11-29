namespace Generic.Domain.Repositories.User
{
    public interface IReadOnlyUserRepository
    {
        Task<Entities.UserEntity?> GetUserByEmail(string email);
        Task<bool> EmailAlreadyUsed(string email);
        Task<string?> GetUserPwdById(int userId);
    }
}
