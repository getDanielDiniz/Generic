namespace Generic.Domain.Repositories.User
{
    public interface IWriteOnlyUserRepository
    {
        Task CreateUser(Entities.UserEntity user);
        Task SetNewPassword(int id, string newPassword);
    }
}
